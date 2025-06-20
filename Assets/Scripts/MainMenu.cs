using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Level_0");
    }

    public void ContinueGame()
    {
        string sceneName = PlayerPrefs.GetString("LastScene", "Level_0");
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Игра завершена");
    }
}
