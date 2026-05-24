using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    public static ScoreCounter Instance;

    public TMP_Text ScoreText;
    public int CurrentScore = 0;

    public void IncreaseScore(int v)
    {
        CurrentScore += v;
        ScoreText.text = "SCORE: " + CurrentScore.ToString();
    }

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        ScoreText.text = "SCORE: " + CurrentScore.ToString();
    }

}
