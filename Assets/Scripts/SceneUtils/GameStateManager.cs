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
        
        // Уничтожаем все объекты с DontDestroyOnLoad
        DestroyAllPersistentObjects();
        
        // Сбрасываем состояние камеры
        ResetCameraState();
        
        // Перезагружаем текущую сцену
        string currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        UnityEngine.SceneManagement.SceneManager.LoadScene(currentSceneName);
    }

    public void ResetToMainMenu()
    {
        Debug.Log("[GameStateManager] ResetToMainMenu called");
        
        // Уничтожаем все объекты с DontDestroyOnLoad
        DestroyAllPersistentObjects();
        
        // Загружаем главное меню
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main_Menu");
    }
    
    private void DestroyAllPersistentObjects()
    {
        // Уничтожаем персонажа
        if (Player.Instance != null)
        {
            Destroy(Player.Instance.gameObject);
        }
        
        // Уничтожаем инвентарь
        InventoryUI[] inventoryUIs = FindObjectsOfType<InventoryUI>();
        foreach (var inventory in inventoryUIs)
        {
            Destroy(inventory.gameObject);
        }
        
        // Уничтожаем колчан
        Quiver[] quivers = FindObjectsOfType<Quiver>();
        foreach (var quiver in quivers)
        {
            Destroy(quiver.gameObject);
        }
        
        // Уничтожаем экран смерти
        DeathScreen[] deathScreens = FindObjectsOfType<DeathScreen>();
        foreach (var deathScreen in deathScreens)
        {
            Destroy(deathScreen.gameObject);
        }
        
        // Уничтожаем все объекты с PlayerPersistence
        PlayerPersistence[] playerPersistences = FindObjectsOfType<PlayerPersistence>();
        foreach (var persistence in playerPersistences)
        {
            Destroy(persistence.gameObject);
        }
        
        Debug.Log("[GameStateManager] All persistent objects destroyed");
    }

    public void DestroySelfAfterSceneLoad()
    {
        Debug.Log("[GameStateManager] DestroySelfAfterSceneLoad called");
        Destroy(gameObject);
    }
    
    // Метод для вызова из OnClick в инспекторе
    public void OnMainMenuButtonClick()
    {
        Debug.Log("[GameStateManager] OnMainMenuButtonClick called from OnClick");
        ResetToMainMenu();
    }
    
    // Метод для рестарта из OnClick в инспекторе
    public void OnRestartButtonClick()
    {
        Debug.Log("[GameStateManager] OnRestartButtonClick called from OnClick");
        RestartLevel();
    }
} 