using UnityEngine;

public class JunkController : MonoBehaviour, IPoolable
{
    [Header("Damage")]
    [SerializeField] int JunkDamage;

    private ObjectPool pooling;

    public void OnSpawn() { }
    public void OnReturn() { }

    public void Init(ObjectPool pool) => pooling = pool;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            collision.gameObject.GetComponent<PlayerHealth>()?.TakeDamage(JunkDamage);

        pooling?.Return(gameObject);
    }
}