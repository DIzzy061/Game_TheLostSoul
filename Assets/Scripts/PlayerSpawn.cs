using UnityEngine;

public class SceneSpawnManager : MonoBehaviour
{
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        GameObject spawnPoint = GameObject.FindWithTag("SpawnPoint");

        // Если пришли с Level_0, спавним на SpawnPoint
        if (player != null)
        {
            if (GameState.PreviousSceneName == "Level_0" && spawnPoint != null)
            {
                player.transform.position = spawnPoint.transform.position;
            }
            else if (GameState.LastPlayerPosition != Vector3.zero)
            {
                player.transform.position = GameState.LastPlayerPosition;
            }
        }
    }
}