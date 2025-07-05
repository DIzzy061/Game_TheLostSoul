using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        if (GameStateManager.Instance != null)
            GameStateManager.Instance.ResetCameraState();
        SceneManager.LoadScene(sceneName);
    }

    public void LoadNextLevel(int currentLevel)
    {
        string nextScene = $"Level_{currentLevel + 1}_Present";
        if (GameStateManager.Instance != null)
            GameStateManager.Instance.ResetCameraState();
        SceneManager.LoadScene(nextScene);
    }

    public void LoadMainMenu()
    {
        if (GameStateManager.Instance != null)
            GameStateManager.Instance.ResetCameraState();
        SceneManager.LoadScene("MainMenu");
    }
}
