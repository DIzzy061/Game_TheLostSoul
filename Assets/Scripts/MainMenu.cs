using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // Сбрасываем состояние игры при начале новой игры
        if (GameStateManager.Instance != null)
        {
            GameStateManager.Instance.ResetGameState();
        }
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
        Debug.Log("���� ���������");
    }

    public void LoadGame()
    {
        // Пока что просто загружаем первый уровень
        // В будущем здесь можно добавить систему сохранений
        SceneManager.LoadScene("Level_0");
    }

    public void OpenSettings()
    {
        // Пока что просто выводим сообщение
        // В будущем здесь можно открыть панель настроек
        Debug.Log("Settings opened");
    }
}
