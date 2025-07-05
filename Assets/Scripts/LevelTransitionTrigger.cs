using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransitionTrigger : MonoBehaviour
{
    [Tooltip("�������� ��������� �����")]
    public string nextSceneName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameState.PreviousSceneName = SceneManager.GetActiveScene().name;
            GameState.LastPlayerPosition = other.transform.position;
            if (GameStateManager.Instance != null)
                GameStateManager.Instance.ResetCameraState();
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
