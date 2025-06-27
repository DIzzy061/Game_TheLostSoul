using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class DeathScreen : MonoBehaviour
{
    public static DeathScreen Instance { get; private set; }

    [Header("UI Elements")]
    public GameObject deathScreenPanel;
    public Button restartButton;
    public Button mainMenuButton;
    public TextMeshProUGUI deathText;
    
    [Header("Settings")]
    public float fadeInDuration = 1f;
    
    private CanvasGroup canvasGroup;
    private bool isDeathScreenActive = false;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    
    void Start()
    {
        canvasGroup = deathScreenPanel.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = deathScreenPanel.AddComponent<CanvasGroup>();
        }
        
        HideDeathScreen();
        
        if (restartButton != null)
            restartButton.onClick.AddListener(RestartLevel);
            
        if (mainMenuButton != null)
            mainMenuButton.onClick.AddListener(ReturnToMainMenu);
          
    }
    
    void Update()
    {
        if (!isDeathScreenActive)
        {
            Health playerHealth = FindObjectOfType<Health>();
            if (playerHealth != null && playerHealth.currentHealth <= 0)
            {
                ShowDeathScreen();
            }
        }
    }
    
    public void ShowDeathScreen()
    {
        if (isDeathScreenActive) return;
        
        isDeathScreenActive = true;
        deathScreenPanel.SetActive(true);
        
        StartCoroutine(FadeIn());
        
        Time.timeScale = 0.4f;
    }
    
    public void HideDeathScreen()
    {
        isDeathScreenActive = false;
        deathScreenPanel.SetActive(false);
        canvasGroup.alpha = 0f;
        
        Time.timeScale = 1f;
    }
    
    private System.Collections.IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        
        while (elapsedTime < fadeInDuration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeInDuration);
            yield return null;
        }
        
        canvasGroup.alpha = 1f;
    }
    
    public void RestartLevel()
    {
        Time.timeScale = 1f;
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        
        SceneManager.LoadScene("Main_Menu");
    }
} 