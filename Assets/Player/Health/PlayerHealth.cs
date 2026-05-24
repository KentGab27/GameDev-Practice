using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] int maxHealth;
    [SerializeField] int currentHealth;
    [SerializeField] public HealthUI HealthUI;

    [Header("Flash Settings")]
    [SerializeField] float flashDuration;

    private SpriteRenderer spriteRender;

   
    public void TakeDamage(int amount)
    {
        currentHealth = Mathf.Max(currentHealth - amount, 0);
        HealthUI.UpdateHearts(currentHealth);

        StartCoroutine(FlashRed());

        if (currentHealth <= 0)
            Die();
    }

    void Start()
    {
        spriteRender = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
        HealthUI.SetMaxHearts(maxHealth);
    }

    void Die()
    {
        Destroy(this.gameObject);
        Time.timeScale = 0f;
        Debug.Log("Player died.");
    }

    private IEnumerator FlashRed()
    {
        spriteRender.color = Color.red;
        yield return new WaitForSeconds(flashDuration);
        spriteRender.color = Color.white;
    }
}