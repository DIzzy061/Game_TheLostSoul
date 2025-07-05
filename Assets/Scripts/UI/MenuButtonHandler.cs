using UnityEngine;
using UnityEngine.UI;

public class MenuButtonHandler : MonoBehaviour
{
    [Header("Button Types")]
    public bool isPlayButton = false;
    public bool isRestartButton = false;
    public bool isMainMenuButton = false;
    public bool isExitButton = false;

    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
    }

    void OnButtonClick()
    {
        if (isPlayButton)
        {
            // Начать новую игру
            if (GameStateManager.Instance != null)
            {
                GameStateManager.Instance.ResetGameState();
            }
            UnityEngine.SceneManagement.SceneManager.LoadScene("Level_0");
        }
        else if (isRestartButton)
        {
            // Перезапустить уровень
            if (GameStateManager.Instance != null)
            {
                GameStateManager.Instance.RestartLevel();
            }
            else
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
            }
        }
        else if (isMainMenuButton)
        {
            // Вернуться в главное меню
            if (GameStateManager.Instance != null)
            {
                GameStateManager.Instance.ResetToMainMenu();
            }
            else
            {
                // Fallback если GameStateManager недоступен
                UnityEngine.SceneManagement.SceneManager.LoadScene("Main_Menu");
            }
        }
        else if (isExitButton)
        {
            // Выйти из игры
            Application.Quit();
            Debug.Log("Игра закрыта");
        }
    }

    void OnDestroy()
    {
        if (button != null)
        {
            button.onClick.RemoveListener(OnButtonClick);
        }
    }
} 