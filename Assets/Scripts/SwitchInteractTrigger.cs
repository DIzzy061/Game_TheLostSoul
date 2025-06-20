using Cainos.PixelArtPlatformer_Dungeon;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(BoxCollider2D))]
public class SwitchInteractTrigger : MonoBehaviour
{
    [Header("Main")]
    public Switch targetSwitch; // Перетащите сюда основной объект рычага

    [Header("Visuals")]
    public GameObject interactPrompt;
    public Vector3 promptOffset = new Vector3(0, 1.5f, 0);

    private bool playerInRange;
    private PlayerInput playerInput;

    private void Awake()
    {
        // Автопоиск если забыли назначить
        if (targetSwitch == null)
            targetSwitch = GetComponentInParent<Switch>();

        // Настройка коллайдера
        var col = GetComponent<BoxCollider2D>();
        col.isTrigger = true;
        col.size = new Vector2(1.5f, 2f);

        // Поиск PlayerInput на игроке
        playerInput = GameObject.FindWithTag("Player")?.GetComponent<PlayerInput>();

        if (interactPrompt)
            interactPrompt.SetActive(false);
    }

    private void Update()
    {
        // Два варианта на случай проблем с Input System
        if (playerInRange && (Input.GetKeyDown(KeyCode.E) ||
            (playerInput?.actions["Interact"]?.triggered ?? false)))
        {
            ToggleSwitch();
        }

        // Обновляем позицию подсказки
        if (interactPrompt && interactPrompt.activeSelf)
            interactPrompt.transform.position =
                Camera.main.WorldToScreenPoint(transform.position + promptOffset);
    }

    public LeverSequenceManager sequenceManager;
    public int leverId;
    private void ToggleSwitch()
    {
        if (targetSwitch == null)
        {
            Debug.LogError("Target Switch is missing!", this);
            return;
        }

        if (!targetSwitch.IsOn)
        {
            targetSwitch.IsOn = true;
            sequenceManager?.RegisterLeverPress(leverId, targetSwitch);
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
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, GetComponent<BoxCollider2D>().size);
    }
#endif
}