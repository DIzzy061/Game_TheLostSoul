using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
public class SwitchLevelTransitionTrigger : MonoBehaviour
{
    [Header("Main")]
    public string sceneToLoad;
    public Cainos.PixelArtPlatformer_Dungeon.Switch targetSwitch;

    [Header("Visuals")]
    public GameObject interactPrompt;
    public Vector3 promptOffset = new Vector3(0, 1.5f, 0);

    private bool playerInRange;
    private PlayerInput playerInput;

    private void Awake()
    {
        if (targetSwitch == null)
            targetSwitch = GetComponentInParent<Cainos.PixelArtPlatformer_Dungeon.Switch>();

        var col = GetComponent<BoxCollider2D>();
        col.isTrigger = true;
        col.size = new Vector2(1.5f, 2f);

        playerInput = GameObject.FindWithTag("Player")?.GetComponent<PlayerInput>();

        if (interactPrompt)
            interactPrompt.SetActive(false);
            
        Debug.Log($"[{gameObject.name}] SwitchLevelTransitionTrigger инициализирован. SceneToLoad: {sceneToLoad}");
    }

    private void Update()
    {
        if (playerInRange && (Input.GetKeyDown(KeyCode.E) ||
            (playerInput?.actions["Interact"]?.triggered ?? false)))
        {
            Debug.Log($"[{gameObject.name}] Кнопка взаимодействия нажата!");
            TriggerSceneChange();
        }

        if (interactPrompt && interactPrompt.activeSelf)
            interactPrompt.transform.position =
                Camera.main.WorldToScreenPoint(transform.position + promptOffset);
    }

    private void TriggerSceneChange()
    {
        Debug.Log($"[{gameObject.name}] TriggerSceneChange вызван");
        
        if (targetSwitch != null)
        {
            Debug.Log($"[{gameObject.name}] Переключаем TargetSwitch");
            targetSwitch.IsOn = !targetSwitch.IsOn;
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
            GameState.LastPlayerPosition = GameObject.FindWithTag("Player").transform.position;
            if (GameStateManager.Instance != null)
                GameStateManager.Instance.ResetCameraState();
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"[{gameObject.name}] Игрок вошел в зону триггера");
            playerInRange = true;
            if (interactPrompt) interactPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"[{gameObject.name}] Игрок вышел из зоны триггера");
            playerInRange = false;
            if (interactPrompt) interactPrompt.SetActive(false);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(transform.position, GetComponent<BoxCollider2D>().size);
    }
#endif
}
