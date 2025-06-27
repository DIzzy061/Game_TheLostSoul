using UnityEngine;

public class TrapKill : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Health playerHealth = other.GetComponent<Health>();
        if (playerHealth != null)
        {
            playerHealth.ApplyDamage(playerHealth.currentHealth); // Сразу убивает
            Debug.Log("Игрок погиб от смертельной ловушки!");
        }
    }
} 