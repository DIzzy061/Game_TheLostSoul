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
    }

    private void Update()
    {
        if (playerInRange && (Input.GetKeyDown(KeyCode.E) ||
            (playerInput?.actions["Interact"]?.triggered ?? false)))
        {
            TriggerSceneChange();
        }

        if (interactPrompt && interactPrompt.activeSelf)
            interactPrompt.transform.position =
                Camera.main.WorldToScreenPoint(transform.position + promptOffset);
    }

    private void TriggerSceneChange()
    {
        if (targetSwitch != null)
        {
            targetSwitch.IsOn = !targetSwitch.IsOn;
        }

        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogWarning("Scene name not assigned in SwitchLevelTransitionTrigger.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (interactPrompt) interactPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
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
