using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreScreenDisplay : MonoBehaviour
{
    public TMP_Text PointsText;

    public void Setup(int score)
    {
        gameObject.SetActive(true);
        int maxScore = PlayerPrefs.GetInt("MaxScore", 0);
        if (score > maxScore)
        {
            maxScore = score;
            PlayerPrefs.SetInt("MaxScore", maxScore);
            PlayerPrefs.Save();
        }

        PointsText.text = ("Score: ") + maxScore;
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameScene");
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
