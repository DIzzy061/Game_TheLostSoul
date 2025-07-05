using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SimpleDoorLevelLoader : MonoBehaviour
{
    public string nextLevelName;
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
        if (!playerNearby || string.IsNullOrEmpty(nextLevelName)) return;
        Debug.Log("Пытаюсь загрузить сцену: " + nextLevelName);
        SceneManager.LoadScene(nextLevelName);
    }

    private void Update()
    {
        Debug.Log("Update работает");
        if (playerNearby)
            Debug.Log("Игрок рядом с дверью");
        if (playerNearby && Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            Debug.Log("E нажата, сбрасываю GameState и пытаюсь загрузить сцену: " + nextLevelName);
            // Сброс GameState перед загрузкой сцены
            GameState.LastPlayerPosition = Vector3.zero;
            GameState.PreviousSceneName = "";
            SceneManager.LoadScene(nextLevelName);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D вызван");
        if (other.CompareTag("Player"))
        {
            Debug.Log("Игрок вошёл в триггер двери");
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