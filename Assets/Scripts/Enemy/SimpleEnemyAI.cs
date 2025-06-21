using UnityEngine;
using System.Collections;

public class SimpleEnemyAI : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float attackRange = 1f;
    public float attackCooldown = 1f;
    public float damage = 10f;

    private Transform player;
    private float lastAttackTime;
    private Animator animator;
    private float groundCheckDistance = 0.1f; // Можно подстроить при необходимости

    public void OnFootstep(AnimationEvent evt) { }
    public void OnAttackStart(AnimationEvent evt) { }
    public void OnAttackHit(AnimationEvent evt) { }
    public void OnAttackCast(AnimationEvent evt) { }
    public void OnAttackEnd(AnimationEvent evt) { }
    public void OnThrow() { }
    public void OnArrowDraw() { }
    public void OnArrowNock() { }
    public void OnArrowReady() { }
    public void OnArrowPutBack() { }
    public void OnLedgeClimbLocked() { }
    public void OnLedgeClimbFinised() { }
    public void OnLadderEntered() { }
    public void OnLadderExited() { }
    public void OnCrawlEnter() { }
    public void OnCrawlEntered() { }
    public void OnCrawlExit() { }
    public void OnCrawlExited() { }
    public void OnDodgeStart() { }
    public void OnDodgeEnd() { }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        animator = GetComponentInChildren<Animator>();
        if (animator == null)
            Debug.LogWarning("Animator не найден!");
    }

    void Update()
    {
        if (animator == null || player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);
        Vector2 direction = (player.position - transform.position).normalized;

        if (distance > attackRange)
        {
            transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;

            // Флип только у корня!
            if (direction.x != 0)
            {
                Vector3 scale = transform.localScale;
                scale.x = Mathf.Sign(direction.x) * Mathf.Abs(scale.x);
                transform.localScale = scale;
            }

            animator.SetFloat("MoveBlend", 1f);
            animator.SetBool("IsMoving", true);
            animator.SetBool("IsAttacking", false);
            animator.SetFloat("VelocityX", direction.x * moveSpeed);
            animator.SetFloat("VelocityY", direction.y * moveSpeed);
            animator.SetBool("IsGrounded", IsOnGround());
            animator.SetBool("IsDead", false);
        }
        else
        {
            animator.SetFloat("MoveBlend", 0f);
            animator.SetBool("IsMoving", false);
            animator.SetFloat("VelocityX", 0f);
            animator.SetFloat("VelocityY", 0f);
            animator.SetBool("IsGrounded", IsOnGround());
            animator.SetBool("IsDead", false);

            if (Time.time - lastAttackTime >= attackCooldown)
            {
                Attack();
                lastAttackTime = Time.time;
            }
        }
    }

    private bool IsOnGround()
    {
        Vector2 position = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(position, Vector2.down, groundCheckDistance);
        // Проверяем, что найден коллайдер и это не сам враг
        return hit.collider != null && hit.collider.gameObject != gameObject;
    }

    void Attack()
    {
        if (player == null) return;

        var health = player.GetComponent<Health>();
        if (health != null)
            health.ApplyDamage(damage);

        if (animator != null)
        {
            animator.SetInteger("AttackAction", 1);
            animator.SetTrigger("Attack");
            animator.SetBool("IsAttacking", true);
            StartCoroutine(ResetIsAttacking());
        }
    }

    IEnumerator ResetIsAttacking()
    {
        yield return new WaitForSeconds(0.2f);
        if (animator != null)
            animator.SetBool("IsAttacking", false);
    }
}
