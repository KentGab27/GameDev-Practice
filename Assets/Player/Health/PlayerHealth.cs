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

    [Header("Audio Settings")]
    [SerializeField] AudioClip damageSoundClip;

    [SerializeField] ScoreScreenDisplay scoreScreenDisplay;

    private SpriteRenderer spriteRender;
    private AudioSource audioSource;
   
    public void TakeDamage(int amount)
    {
        currentHealth = Mathf.Max(currentHealth - amount, 0);
        HealthUI.UpdateHearts(currentHealth);

        AudioSoundManager.Instance?.PlayHurtSound();

        StartCoroutine(FlashRed());

        if (currentHealth <= 0)
        {
            Die();
        }      
    }

    void Start()
    {
        spriteRender = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
        HealthUI.SetMaxHearts(maxHealth);
    }

    void Die()
    {
        AudioSoundManager.Instance?.PlayLoseSound();

        int score = ScoreCounter.Instance != null ? ScoreCounter.Instance.CurrentScore : 0;

        scoreScreenDisplay.Setup(score);

        Time.timeScale = 0f;
        gameObject.SetActive(false);
    }

    private IEnumerator FlashRed()
    {
        spriteRender.color = Color.red;
        yield return new WaitForSeconds(flashDuration);
        spriteRender.color = Color.white;
    }
}