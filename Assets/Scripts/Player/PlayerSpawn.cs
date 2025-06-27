using UnityEngine;

public class SceneSpawnManager : MonoBehaviour
{
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        GameObject spawnPoint = GameObject.FindWithTag("SpawnPoint");

        if (player != null)
        {
            if (GameState.PreviousSceneName == "Level_0" && spawnPoint != null)
            {
                Debug.Log($"Игрок спавнится на SpawnPoint (пришел с {GameState.PreviousSceneName})");
                player.transform.position = spawnPoint.transform.position;
            }
            else if (GameState.LastPlayerPosition != Vector3.zero)
            {
                Debug.Log($"Игрок восстанавливается на позиции {GameState.LastPlayerPosition} (пришел с {GameState.PreviousSceneName})");
                player.transform.position = GameState.LastPlayerPosition;
            }
            else if (spawnPoint != null)
            {
                Debug.Log($"Игрок спавнится на SpawnPoint (нет сохраненной позиции)");
                player.transform.position = spawnPoint.transform.position;
            }
        }
    }
}