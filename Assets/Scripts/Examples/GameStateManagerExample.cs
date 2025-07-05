using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Пример использования GameStateManager
/// Этот скрипт демонстрирует, как использовать систему управления состоянием игры
/// </summary>
public class GameStateManagerExample : MonoBehaviour
{
    [Header("UI Elements")]
    public Button restartButton;
    public Button mainMenuButton;
    public Button testButton;

    void Start()
    {
        // Настраиваем кнопки
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(OnRestartClick);
        }

        if (mainMenuButton != null)
        {
            mainMenuButton.onClick.AddListener(OnMainMenuClick);
        }

        if (testButton != null)
        {
            testButton.onClick.AddListener(OnTestClick);
        }

        // Проверяем, что GameStateManager существует
        if (GameStateManager.Instance == null)
        {
            Debug.LogWarning("GameStateManager не найден! Создаем новый...");
            GameObject gameStateManagerObj = new GameObject("GameStateManager");
            gameStateManagerObj.AddComponent<GameStateManager>();
        }
    }

    void OnRestartClick()
    {
        Debug.Log("Нажата кнопка Restart");
        if (GameStateManager.Instance != null)
        {
            GameStateManager.Instance.RestartLevel();
        }
        else
        {
            Debug.LogError("GameStateManager не найден!");
        }
    }

    void OnMainMenuClick()
    {
        Debug.Log("Нажата кнопка Вернуться в меню");
        if (GameStateManager.Instance != null)
        {
            GameStateManager.Instance.ResetToMainMenu();
        }
        else
        {
            Debug.LogError("GameStateManager не найден!");
        }
    }

    void OnTestClick()
    {
        Debug.Log("Нажата тестовая кнопка - сброс только статических данных");
        if (GameStateManager.Instance != null)
        {
            GameStateManager.Instance.ResetGameState();
            Debug.Log("Статические данные сброшены");
        }
        else
        {
            Debug.LogError("GameStateManager не найден!");
        }
    }

    void OnDestroy()
    {
        // Очищаем слушатели событий
        if (restartButton != null)
        {
            restartButton.onClick.RemoveListener(OnRestartClick);
        }

        if (mainMenuButton != null)
        {
            mainMenuButton.onClick.RemoveListener(OnMainMenuClick);
        }

        if (testButton != null)
        {
            testButton.onClick.RemoveListener(OnTestClick);
        }
    }
} 