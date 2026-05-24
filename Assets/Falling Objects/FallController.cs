using UnityEngine;

public class FallController : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] float startSpawnInterval = 1.5f;
    [SerializeField] float endSpawnInterval = 0.4f;
    [SerializeField] float spawnHeightOffset = 1f;
    [SerializeField] int startObjectsPerSpawn = 1;
    [SerializeField] int endObjectsPerSpawn = 3;

    [Header("Fall Settings")]
    [SerializeField] float startGravityScale = 1f;
    [SerializeField] float endGravityScale = 3f;

    [Header("References")]
    [SerializeField] TimerController timer;
    [SerializeField] ObjectPool[] fallingObjectPools;

    private const float leftViewportX = 0f;
    private const float rightViewportX = 1f;
    private const float bottomViewportY = 0f;
    private const float topViewportY = 1f;
    private const float viewportZ = 0f;

    private float spawnTimer;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        float currentSpawnInterval = GetCurrentSpawnInterval();

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= currentSpawnInterval)
        {
            spawnTimer = 0f;
            SpawnWave();
        }
    }

    void SpawnWave()
    {
        if (fallingObjectPools == null || fallingObjectPools.Length == 0) return;
        if (cam == null) return;

        int objectsToSpawn = GetCurrentObjectsPerSpawn();

        for (int i = 0; i < objectsToSpawn; i++)
        {
            Spawn();
        }
    }

    void Spawn()
    {
        ObjectPool pool = fallingObjectPools[Random.Range(0, fallingObjectPools.Length)];

        float spawnY = cam.ViewportToWorldPoint(new Vector3(viewportZ, topViewportY, cam.nearClipPlane)).y + spawnHeightOffset;
        float leftX = cam.ViewportToWorldPoint(new Vector3(leftViewportX, bottomViewportY, cam.nearClipPlane)).x;
        float rightX = cam.ViewportToWorldPoint(new Vector3(rightViewportX, bottomViewportY, cam.nearClipPlane)).x;

        GameObject spawnedObject = pool.Get(
            new Vector3(Random.Range(leftX, rightX), spawnY, viewportZ),
            Quaternion.identity
        );

        if (spawnedObject.TryGetComponent(out Rigidbody2D rb))
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.gravityScale = GetCurrentGravityScale();
        }
    }

    private float GetTimeProgress()
    {
        if (timer == null) return 0f;

        return timer.TimeProgress;
    }

    private float GetCurrentSpawnInterval()
    {
        return Mathf.Lerp(startSpawnInterval, endSpawnInterval, GetTimeProgress());
    }

    private float GetCurrentGravityScale()
    {
        return Mathf.Lerp(startGravityScale, endGravityScale, GetTimeProgress());
    }

    private int GetCurrentObjectsPerSpawn()
    {
        int maxObjects = Mathf.RoundToInt(
            Mathf.Lerp(startObjectsPerSpawn, endObjectsPerSpawn, GetTimeProgress())
        );

        maxObjects = Mathf.Max(1, maxObjects);

        return Random.Range(1, maxObjects + 1);
    }
}