using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ResetCameraState()
    {
        Debug.Log("[GameStateManager] ResetCameraState called");
    }

    public void ResetGameState()
    {
        Debug.Log("[GameStateManager] ResetGameState called");
    }

    public void RestartLevel()
    {
        Debug.Log("[GameStateManager] RestartLevel called");
    }

    public void ResetToMainMenu()
    {
        Debug.Log("[GameStateManager] ResetToMainMenu called");
    }

    public void DestroySelfAfterSceneLoad()
    {
        Debug.Log("[GameStateManager] DestroySelfAfterSceneLoad called");
        Destroy(gameObject);
    }
} 