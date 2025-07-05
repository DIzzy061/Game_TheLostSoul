using UnityEngine;

public class GameStateManagerInitializer : MonoBehaviour
{
    void Start()
    {
        if (GameStateManager.Instance == null)
        {
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