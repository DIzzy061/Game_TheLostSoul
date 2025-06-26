using UnityEngine;

public class ArrowPickup : MonoBehaviour
{
    public int arrowsAmount = 5; // Сколько стрел даёт этот предмет

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (Quiver.Instance != null)
            {
                Quiver.Instance.AddArrows(arrowsAmount);
                Destroy(gameObject);
            }
        }
    }
} 