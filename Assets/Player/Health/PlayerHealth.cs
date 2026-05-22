using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;

    public HealthUI HealthUI;

    void Start()
    {
        currentHealth = maxHealth;
        HealthUI.SetMaxHearts(maxHealth);
    }

    public void TakeDamage(int amount)
    {
        currentHealth = Mathf.Max(currentHealth - amount, 0);
        HealthUI.UpdateHearts(currentHealth);

        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        Destroy(this.gameObject);
        Time.timeScale = 0f;
        Debug.Log("Player died.");
    }
}