using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [Header("Heart Images")]
    public Image HeartPrefab;
    public Sprite FullHeartSprite;
    public Sprite EmptyHeartSprite;

    private List<Image> hearts = new List<Image>();

    public void SetMaxHearts(int maxHearts)
    {
        foreach(Image Heart in hearts)
        {
            Destroy(Heart.gameObject);
        }

        hearts.Clear();

        for (int i = 0; i < maxHearts; i++)
        {
            Image newHeart = Instantiate(HeartPrefab, transform);
            newHeart.sprite = FullHeartSprite;
            newHeart.color = Color.red;
            hearts.Add(newHeart);
        }
    }

    public void UpdateHearts(int currentHealth)
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].sprite = FullHeartSprite;
                hearts[i].color = Color.red;
            }
            else
            {
                hearts[i].sprite = EmptyHeartSprite;
                hearts[i].color = Color.white;
            }
        }
    }
}
