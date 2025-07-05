using UnityEngine;

public class TestGameStateManager : MonoBehaviour
{
    void Start()
    {
        Debug.Log("TestGameStateManager: Start вызван");
        
        // Проверяем, существует ли GameStateManager
        if (GameStateManager.Instance != null)
        {
            Debug.Log("TestGameStateManager: GameStateManager найден!");
        }
        else
        {
            Debug.LogError("TestGameStateManager: GameStateManager НЕ найден!");
            
            // Создаем GameStateManager вручную
            GameObject gameStateManagerObj = new GameObject("GameStateManager");
            gameStateManagerObj.AddComponent<GameStateManager>();
            Debug.Log("TestGameStateManager: GameStateManager создан вручную");
        }
    }

    void Update()
    {
        // Тестовая кнопка для проверки
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("TestGameStateManager: Нажата клавиша R - тестируем RestartLevel");
            if (GameStateManager.Instance != null)
            {
                GameStateManager.Instance.RestartLevel();
            }
            else
            {
                Debug.LogError("TestGameStateManager: GameStateManager все еще не найден!");
            }
        }
        
        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("TestGameStateManager: Нажата клавиша M - тестируем ResetToMainMenu");
            if (GameStateManager.Instance != null)
            {
                GameStateManager.Instance.ResetToMainMenu();
            }
            else
            {
                Debug.LogError("TestGameStateManager: GameStateManager все еще не найден!");
            }
        }
    }
} 