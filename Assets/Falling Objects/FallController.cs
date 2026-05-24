using UnityEngine;

public class FallController : MonoBehaviour
{
    private const float LeftViewportX = 0f;
    private const float RightViewportX = 1f;
    private const float BottomViewportY = 0f;
    private const float TopViewportY = 1f;
    private const float ViewportZ = 0f;

    [Header("Spawn Settings")]
    [SerializeField] private float startSpawnInterval = 1.5f;
    [SerializeField] private float endSpawnInterval = 0.4f;
    [SerializeField] private float spawnHeightOffset = 1f;
    [SerializeField] private int startObjectsPerSpawn = 1;
    [SerializeField] private int endObjectsPerSpawn = 3;

    [Header("Fall Settings")]
    [SerializeField] private float startGravityScale = 1f;
    [SerializeField] private float endGravityScale = 3f;

    [Header("References")]
    [SerializeField] private TimerController timer;
    [SerializeField] private ObjectPool[] fallingObjectPools;

    private float spawnTimer;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        float currentSpawnInterval = GetCurrentSpawnInterval();

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= currentSpawnInterval)
        {
            spawnTimer = 0f;
            SpawnWave();
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

    private int GetCurrentObjectsPerSpawn()
    {
        int maxObjects = Mathf.RoundToInt(
            Mathf.Lerp(startObjectsPerSpawn, endObjectsPerSpawn, GetTimeProgress())
        );

        maxObjects = Mathf.Max(1, maxObjects);

        return Random.Range(1, maxObjects + 1);
    }

    private float GetCurrentGravityScale()
    {
        return Mathf.Lerp(startGravityScale, endGravityScale, GetTimeProgress());
    }

    private void SpawnWave()
    {
        if (fallingObjectPools == null || fallingObjectPools.Length == 0) return;
        if (cam == null) return;

        int objectsToSpawn = GetCurrentObjectsPerSpawn();

        for (int i = 0; i < objectsToSpawn; i++)
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        ObjectPool pool = fallingObjectPools[Random.Range(0, fallingObjectPools.Length)];

        float spawnY = cam.ViewportToWorldPoint(new Vector3(ViewportZ, TopViewportY, cam.nearClipPlane)).y + spawnHeightOffset;
        float leftX = cam.ViewportToWorldPoint(new Vector3(LeftViewportX, BottomViewportY, cam.nearClipPlane)).x;
        float rightX = cam.ViewportToWorldPoint(new Vector3(RightViewportX, BottomViewportY, cam.nearClipPlane)).x;

        GameObject spawnedObject = pool.Get(
            new Vector3(Random.Range(leftX, rightX), spawnY, ViewportZ),
            Quaternion.identity
        );

        if (spawnedObject.TryGetComponent(out Rigidbody2D rb))
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.gravityScale = GetCurrentGravityScale();
        }
    }
}