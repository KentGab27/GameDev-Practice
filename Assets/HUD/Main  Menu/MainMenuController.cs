using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private string gameSceneName = "GameScene";
    [SerializeField] private string howToPlaySceneName = "HowToPlay";
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    public void OnStartClick()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void OnHowToPlayClick()
    {
        SceneManager.LoadScene(howToPlaySceneName);
    }

    public void OnExitHowToPlay()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void OnExitClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
