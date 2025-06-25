using UnityEngine;
using Cainos.CustomizablePixelCharacter;
using System.Collections;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    public HealthBar healthBar;

    [Header("   ")]
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
        if (healthBar != null)
            healthBar.SetHealth(1f);
    }

    protected virtual void Update()
    {
        float currentY = transform.position.y;

        if (rb.velocity.y > 0.1f && currentY > highestY)
            highestY = currentY;

        if (rb.velocity.y < -0.1f)
            isFalling = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            float fallDistance = highestY - transform.position.y;
            if (isFalling && fallDistance > fallHeightThreshold)
            {
                ApplyDamage(fallDamage);
                Debug.Log($"Падение с высоты {fallDistance:F2}, урон {fallDamage}");
            }
            isFalling = false;
            highestY = transform.position.y;
        }
    }

    public virtual void ApplyDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log($"HP: {currentHealth}");

        var animator = GetComponentInChildren<Animator>();
        if (animator)
        {
            animator.SetTrigger("InjuredFront");
        }

        if (healthBar != null)
            healthBar.SetHealth(currentHealth / maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Debug.Log("Персонаж погиб!");
        var animator = GetComponentInChildren<Animator>();
        if (animator)
        {
            animator.SetBool("IsDead", true);
        }
        var controller = GetComponent<PixelCharacterController>();
        if (controller) controller.enabled = false;
        var input = GetComponent<PixelCharacterInputMouseAndKeyboard>();
        if (input) input.enabled = false;

        StartCoroutine(FreezeAndLiftAfterDelay(0.2f));
    }

    private System.Collections.IEnumerator FreezeAndLiftAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        transform.position += new Vector3(0, 0.13f, 0);
        var rb = GetComponent<Rigidbody2D>();
        if (rb) rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
}
