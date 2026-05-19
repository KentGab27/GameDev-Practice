using UnityEngine;

public class FoodController : MonoBehaviour, IPoolable
{
    [Header("Point Value")]
    [SerializeField] private int Value;

    private ObjectPool _pool;

    public void OnSpawn() { }
    public void OnReturn() { }

    public void Init(ObjectPool pool) => _pool = pool;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            ScoreCounter.Instance.IncreaseScore(Value);

        _pool?.Return(gameObject);
    }
}