using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    private const string LevelSceneName = "Level";
    private const string MenuSceneName = "Menu";

    public void Restart()
    {
        SceneManager.LoadScene(LevelSceneName);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(LevelSceneName);
    }

    public void OpenMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(MenuSceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}