using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cainos.PixelArtTopDown_Basic;

public class BlockGrabbing : MonoBehaviour
{
    public Transform grabPoint;
    public float grabRange = 1f;
    public float grabOffset = 0.1f;
    public LayerMask grabbableLayer;
    public float directionThreshold = 0.5f;

    public float normalSpeed = 3f;
    public float grabSpeed = 1.5f;

    private GameObject grabbedBlock;
    private bool isGrabbing = false;
    private Vector2 inputDirection = Vector2.right;

    private Rigidbody2D rb;
    private TopDownCharacterController playerMovement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<TopDownCharacterController>();
    }

    void Update()
    {
        UpdateInputDirection();

        if (GameInput.Instance.PlayerInputActions.Player.Interact.triggered)
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

        UpdatePlayerSpeed();
    }

    void UpdateInputDirection()
    {
        if (isGrabbing) return;

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
        float currentGrabRange = grabRange;
        float grabDistance = currentGrabRange + grabOffset;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, currentGrabRange, grabbableLayer);

        foreach (var hit in hits)
        {
            Vector2 toObject = (hit.transform.position - transform.position).normalized;

            if (Vector2.Dot(inputDirection, toObject) >= directionThreshold)
            {
                grabbedBlock = hit.gameObject;

                grabPoint.localPosition = inputDirection * grabDistance;

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

    void UpdatePlayerSpeed()
    {
        if (playerMovement != null)
        {
            playerMovement.speed = isGrabbing ? grabSpeed : normalSpeed;
            playerMovement.freezeDirection = isGrabbing;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, grabRange);
    }
}
