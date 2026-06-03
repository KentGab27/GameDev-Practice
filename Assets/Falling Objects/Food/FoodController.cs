using UnityEngine;

public class FoodController : MonoBehaviour, IPoolable
{
    [Header("Point Value")]
    [SerializeField] int Value;

    private ObjectPool pooling;

    public void OnSpawn() { }
    public void OnReturn() { }

    public void Init(ObjectPool pool) => pooling = pool;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ScoreCounter.Instance.IncreaseScore(Value);
            AudioSoundManager.Instance?.PlayCollectSound();
        }

        pooling?.Return(gameObject);
    }
}