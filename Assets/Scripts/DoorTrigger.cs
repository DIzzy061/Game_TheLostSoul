using UnityEngine;
using UnityEngine.InputSystem;
using Cainos.PixelArtPlatformer_Dungeon;

public class DoorInteractTrigger : MonoBehaviour
{
    [SerializeField] private Door door;
    private bool playerNearby = false;
    private GameInput gameInput;

    private void Start()
    {
        gameInput = GameInput.Instance;
        if (gameInput?.PlayerInputActions?.Player != null)
        {
            gameInput.PlayerInputActions.Player.Interact.performed += OnInteract;
        }
    }

    private void OnDisable()
    {
        if (gameInput?.PlayerInputActions?.Player != null)
        {
            gameInput.PlayerInputActions.Player.Interact.performed -= OnInteract;
        }
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        if (!playerNearby || door == null) return;

        if (door.IsOpened)
            door.Close();
        else
            door.Open();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
        }
    }
}