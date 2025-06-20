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
            GameState.LastPlayerPosition = other.transform.position; 
            SceneManager.LoadScene(nextSceneName); 
        }
    }
}
