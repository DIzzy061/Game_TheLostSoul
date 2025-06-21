using UnityEngine;

public class EnemyDamageDealer : MonoBehaviour
{
    public float damage = 25f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Health enemy = collision.GetComponent<Health>();
            if (enemy != null)
            {
                enemy.ApplyDamage(damage);
            }
        }
    }
}
