using UnityEngine;

public class JunkController : MonoBehaviour, IPoolable
{
    [Header("Damage")]
    [SerializeField] private int JunkDamage;

    private ObjectPool _pool;

    public void OnSpawn() { }
    public void OnReturn() { }

    public void Init(ObjectPool pool) => _pool = pool;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            collision.gameObject.GetComponent<PlayerHealth>()?.TakeDamage(JunkDamage);

        _pool?.Return(gameObject);
    }
}