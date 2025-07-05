using UnityEngine;

public class GameStateManagerInitializer : MonoBehaviour
{
    void Start()
    {
        // Проверяем, существует ли уже GameStateManager
        if (GameStateManager.Instance == null)
        {
            // Создаем новый GameStateManager
            GameObject gameStateManagerObj = new GameObject("GameStateManager");
            gameStateManagerObj.AddComponent<GameStateManager>();
            Debug.Log("GameStateManager создан автоматически");
        }
        else
        {
            Debug.Log("GameStateManager уже существует");
        }
    }
} 