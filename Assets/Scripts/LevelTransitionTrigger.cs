using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransitionTrigger : MonoBehaviour
{
    [Tooltip("Level_1_Present")]
    public string nextSceneName;

    private bool hasTransitioned = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasTransitioned && other.CompareTag("Player"))
        {
            hasTransitioned = true;
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
