using UnityEngine;

public class FallController : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private float SpawnInterval = 1.5f;
    [SerializeField] private float SpawnHeightOffset = 1f;
    [SerializeField] private ObjectPool[] FallingObjectPools;

    private Camera _cam;

    private void Start()
    {
        _cam = Camera.main;
        InvokeRepeating(nameof(Spawn), SpawnInterval, SpawnInterval);
    }

    private void Spawn()
    {
        if (FallingObjectPools == null || FallingObjectPools.Length == 0) return;

        ObjectPool pool = FallingObjectPools[Random.Range(0, FallingObjectPools.Length)];

        float spawnY = _cam.ViewportToWorldPoint(new Vector3(0f, 1f, _cam.nearClipPlane)).y + SpawnHeightOffset;
        float leftX = _cam.ViewportToWorldPoint(new Vector3(0f, 0f, _cam.nearClipPlane)).x;
        float rightX = _cam.ViewportToWorldPoint(new Vector3(1f, 0f, _cam.nearClipPlane)).x;

        pool.Get(new Vector3(Random.Range(leftX, rightX), spawnY, 0f), Quaternion.identity);
    }
}