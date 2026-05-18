using UnityEngine;

public class FoodController : MonoBehaviour
{
    public int Value;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            ScoreCounter.Instance.IncreaseScore(Value);

        Destroy(gameObject);
    }
}
