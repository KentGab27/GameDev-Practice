using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{
    [Header("Time Settings")]
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float duration = 80f;

    [Header("Score Screen")]
    [SerializeField] ScoreScreenDisplay scoreScreenDisplay;

    private float elapsedTime;
    private bool timerEnd;

    public float RemainingTime => Mathf.Max(0f, duration - elapsedTime);

    public float TimeProgress
    {
        get
        {
            if (duration <= 0f) return 1f;
            return Mathf.Clamp01(elapsedTime / duration);
        }
    }

    void Update()
    {
        if (timerEnd) return;

        elapsedTime += Time.deltaTime;

        int minutes = Mathf.FloorToInt(RemainingTime / 60);
        int seconds = Mathf.FloorToInt(RemainingTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (RemainingTime <= 0f)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        timerEnd = true;

        int score = ScoreCounter.Instance != null ? ScoreCounter.Instance.CurrentScore : 0;

        scoreScreenDisplay.Setup(score);

        Time.timeScale = 0f;
    }
}