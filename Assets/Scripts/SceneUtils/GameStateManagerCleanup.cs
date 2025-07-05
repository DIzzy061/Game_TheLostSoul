using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManagerCleanup : MonoBehaviour
{
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Main_Menu")
        {
            if (GameStateManager.Instance != null)
            {
                GameStateManager.Instance.DestroySelfAfterSceneLoad();
            }
        }
    }
} 