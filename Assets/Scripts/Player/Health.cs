using UnityEngine;
using Cainos.CustomizablePixelCharacter;
using System.Collections;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    public HealthBar healthBar;

    [Header("Параметры урона от падения")]
    public float fallHeightThreshold = 3f;
    public float minFallDamage = 10f;
    public float maxFallDamage = 50f;
    public float maxFallHeight = 10f;

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
                float t = Mathf.InverseLerp(fallHeightThreshold, maxFallHeight, fallDistance);
                float damage = Mathf.Lerp(minFallDamage, maxFallDamage, t);

                ApplyDamage(damage);
                Debug.Log($"Падение с высоты {fallDistance:F2}, урон {damage:F1}");
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

        var pixelCharacter = GetComponent<PixelCharacter>();
        if (pixelCharacter)
        {
            pixelCharacter.IsEyeCloed = false;
            pixelCharacter.IsDead = true;
        }

        StartCoroutine(FreezeAndLiftAfterDelay(0.2f));
    }

    private System.Collections.IEnumerator FreezeAndLiftAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        transform.position += new Vector3(0, 0.25f, 0);
        var rb = GetComponent<Rigidbody2D>();
        if (rb) rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    private bool IsOnGround()
    {
        Collider2D col = GetComponent<Collider2D>();
        return col != null && col.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }

    public void CancelFall()
    {
        isFalling = false;
        highestY = transform.position.y;
    }
}
