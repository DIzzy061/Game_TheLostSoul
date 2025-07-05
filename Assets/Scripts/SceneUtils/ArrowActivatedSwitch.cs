using UnityEngine;
using UnityEngine.SceneManagement;

public class ArrowActivatedSwitch : MonoBehaviour
{
    [Header("Main")]
    public string sceneToLoad;
    public Cainos.PixelArtPlatformer_Dungeon.Switch targetSwitch;
    
    [Header("Arrow Settings")]
    public string arrowTag = "Arrow";
    public bool canBeActivatedMultipleTimes = false;
    
    private bool isActivated = false;

    private void Awake()
    {
        if (targetSwitch == null)
            targetSwitch = GetComponentInParent<Cainos.PixelArtPlatformer_Dungeon.Switch>();
            
        Debug.Log($"[{gameObject.name}] ArrowActivatedSwitch инициализирован. SceneToLoad: {sceneToLoad}");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"[{gameObject.name}] OnTriggerEnter2D вызван с объектом: {other.name}, тег: {other.tag}");
        
        if (other.CompareTag(arrowTag) && (!isActivated || canBeActivatedMultipleTimes))
        {
            Debug.Log($"[{gameObject.name}] Стрела попала в кнопку!");
            ActivateSwitch();
            
            Destroy(other.gameObject);
        }
        else
        {
            Debug.Log($"[{gameObject.name}] Объект не подходит: тег {other.tag} != {arrowTag}, активирована: {isActivated}");
        }
    }

    private void ActivateSwitch()
    {
        Debug.Log($"[{gameObject.name}] ActivateSwitch вызван");
        
        isActivated = true;
        
        if (targetSwitch != null)
        {
            Debug.Log($"[{gameObject.name}] Активируем TargetSwitch");
            targetSwitch.IsOn = true;
        }

        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            Debug.Log($"[{gameObject.name}] Загружаем сцену: {sceneToLoad}");
            
            bool sceneExists = false;
            for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
                string sceneNameFromPath = System.IO.Path.GetFileNameWithoutExtension(scenePath);
                if (sceneNameFromPath == sceneToLoad)
                {
                    sceneExists = true;
                    break;
                }
            }
            
            if (!sceneExists)
            {
                Debug.LogError($"[{gameObject.name}] Сцена '{sceneToLoad}' не найдена в Build Settings!");
                return;
            }
            
            GameState.PreviousSceneName = SceneManager.GetActiveScene().name;
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                GameState.LastPlayerPosition = player.transform.position;
            }
            
            if (GameStateManager.Instance != null)
                GameStateManager.Instance.ResetCameraState();
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogWarning($"[{gameObject.name}] Scene name not assigned in ArrowActivatedSwitch.");
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, GetComponent<BoxCollider2D>().size);
    }
#endif
} 