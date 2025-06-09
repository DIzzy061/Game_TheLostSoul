using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGrabbing : MonoBehaviour
{
    public Transform grabPoint;               // точка, куда перемещается блок
    public float grabRange = 1f;              // радиус поиска блока
    public LayerMask grabbableLayer;          // слой перетаскиваемых блоков
    public float directionThreshold = 0.5f;   // насколько строго проверять направление

    private GameObject grabbedBlock;
    private bool isGrabbing = false;
    private Vector2 inputDirection = Vector2.right;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        UpdateInputDirection();

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isGrabbing)
                TryGrab();
            else
                Release();
        }

        if (isGrabbing && grabbedBlock != null)
        {
            grabbedBlock.GetComponent<Rigidbody2D>().MovePosition(grabPoint.position);
        }
    }

    void UpdateInputDirection()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector2 newDirection = new Vector2(h, v).normalized;
        if (newDirection != Vector2.zero)
        {
            inputDirection = newDirection;

        }
    }

    void TryGrab()
    {
        // ищем все коллайдеры в радиусе
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, grabRange, grabbableLayer);

        foreach (var hit in hits)
        {
            Vector2 toObject = (hit.transform.position - transform.position).normalized;

            // проверка направления взгляда
            if (Vector2.Dot(inputDirection, toObject) >= directionThreshold)
            {
                grabbedBlock = hit.gameObject;
                grabPoint.localPosition = inputDirection * 1f;
                isGrabbing = true;

                var blockRb = grabbedBlock.GetComponent<Rigidbody2D>();
                blockRb.isKinematic = true;
                return;
            }
        }
    }

    void Release()
    {
        if (grabbedBlock != null)
        {
            grabbedBlock.GetComponent<Rigidbody2D>().isKinematic = false;
        }

        grabbedBlock = null;
        isGrabbing = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, grabRange);
    }
}
