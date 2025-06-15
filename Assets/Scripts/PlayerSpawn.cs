using UnityEngine;

public class SceneSpawnManager : MonoBehaviour
{
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null && GameState.LastPlayerPosition != Vector3.zero)
        {
            player.transform.position = GameState.LastPlayerPosition;
        }
    }
}