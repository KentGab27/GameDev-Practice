using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{
    [Header("Time Settings")]
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float duration = 80f;

    private float elapsedTime;

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
        elapsedTime += Time.deltaTime;

        int minutes = Mathf.FloorToInt(RemainingTime / 60);
        int seconds = Mathf.FloorToInt(RemainingTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}