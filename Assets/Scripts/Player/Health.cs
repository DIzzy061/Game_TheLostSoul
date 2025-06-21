using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    [Header("Порог урона от падения")]
    public float fallHeightThreshold = 5f;
    public float fallDamage = 25f;

    private float highestY;
    private bool isFalling = false;

    private Rigidbody2D rb;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        highestY = transform.position.y;
    }

    protected virtual void Update()
    {
        float currentY = transform.position.y;

        if (rb.velocity.y > 0.1f && currentY > highestY)
            highestY = currentY;

        if (rb.velocity.y < -0.1f)
            isFalling = true;

        if (isFalling && Mathf.Abs(rb.velocity.y) < 0.01f)
        {
            float fallDistance = highestY - currentY;
            if (fallDistance > fallHeightThreshold)
            {
                ApplyDamage(fallDamage);
                Debug.Log($"Падение с высоты {fallDistance:F2}, получен урон {fallDamage}");
            }

            isFalling = false;
            highestY = currentY;
        }
    }

    public virtual void ApplyDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log($"HP: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Debug.Log("Персонаж умер");
        gameObject.SetActive(false);
    }
}
