using UnityEngine;

public class JunkController : MonoBehaviour
{
    [Header("Damage")]
    [SerializeField] int junkDamage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            collision.gameObject.GetComponent<PlayerHealth>()?.TakeDamage(junkDamage);

        Destroy(gameObject);
    }
}