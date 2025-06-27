using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPersistence : MonoBehaviour
{
    private static PlayerPersistence instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Сброс состояния камеры при переходе между сценами
        CameraFollow cameraFollow = GetComponentInChildren<CameraFollow>();
        if (cameraFollow != null)
        {
            // Принудительно сбросить peek и позицию камеры
            cameraFollow.ResetCameraState();
        }
    }
}
