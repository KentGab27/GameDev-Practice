using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] int maxHealth;

    public Sprite EmptyHeart;
    public Sprite FullHeart;
    public Image[] Hearts;

    public SpriteRenderer PlayerSpriteRender;
    public PlayerController PlayerMovement;

    public PlayerHealth PlayHealth;

    public void TakeDamage(int amount)
    {
        health -= amount;
        if(health <= 0)
        {
            PlayerSpriteRender.enabled = false;
            PlayerMovement.enabled = false;
        }
    }

    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {

        for (int i = 0; i < Hearts.Length; i++)
        {
            if (i < health)
            {
                Hearts[i].sprite = FullHeart;
            }
            else
            {
                Hearts[i].sprite = EmptyHeart;
            }

            if (i < maxHealth)
            {
                Hearts[i].enabled = true;
            }
            else
            {
                Hearts[i].enabled = false;
            }
        }
    }
}
