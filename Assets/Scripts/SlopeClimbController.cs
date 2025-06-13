using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class SlopeClimbController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float slopeDetectionRayLength = 0.1f;
    public LayerMask rampLayer;

    private Rigidbody2D rb;
    private Collider2D col;
    private bool onRamp = false;
    private float horizontalInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        // Проверка наличия наклона под персонажем
        Vector2 origin = col.bounds.center;
        Vector2 direction = Vector2.down;

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, slopeDetectionRayLength, rampLayer);

        if (hit.collider != null)
        {
            // Если поверхность под углом
            float angle = Vector2.Angle(hit.normal, Vector2.up);

            if (angle > 0.1f && angle < 45f)
            {
                onRamp = true;
                rb.gravityScale = 0;
            }
            else
            {
                onRamp = false;
                rb.gravityScale = 1;
            }
        }
        else
        {
            onRamp = false;
            rb.gravityScale = 1;
        }
    }

    void FixedUpdate()
    {
        float verticalVelocity = onRamp ? 0 : rb.velocity.y;
        rb.velocity = new Vector2(horizontalInput * moveSpeed, verticalVelocity);
    }
}
