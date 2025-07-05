using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cainos.PixelArtTopDown_Basic;

public class BlockGrabbing : MonoBehaviour
{
    public Transform grabPoint;
    public float grabRange = 1f;
    public float grabOffsetX = 0.5f;
    public float grabOffsetY = 0.3f;
    public LayerMask grabbableLayer;

    public float normalSpeed = 3f;
    public float grabSpeed = 1.5f;

    private GameObject grabbedBlock;
    private bool isGrabbing = false;

    private Rigidbody2D rb;
    private TopDownCharacterController playerMovement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<TopDownCharacterController>();
    }

    void Update()
    {
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

    void TryGrab()
    {
        float currentGrabRange = grabRange;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, currentGrabRange, grabbableLayer);

        float minDist = float.MaxValue;
        GameObject nearestBlock = null;
        foreach (var hit in hits)
        {
            float dist = Vector2.Distance(transform.position, hit.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearestBlock = hit.gameObject;
            }
        }

        if (nearestBlock != null)
        {
            grabbedBlock = nearestBlock;
            Vector2 dir = (grabbedBlock.transform.position - transform.position).normalized;
            Vector2 offset = new Vector2(
                Mathf.Abs(dir.x) > Mathf.Abs(dir.y) ? grabOffsetX : 0,
                Mathf.Abs(dir.y) >= Mathf.Abs(dir.x) ? grabOffsetY : 0
            );
            grabPoint.localPosition = new Vector2(dir.x * offset.x, dir.y * offset.y);

            isGrabbing = true;

            if (playerMovement != null)
            {
                playerMovement.freezeDirection = true;
                int direction = 0;
                if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
                    direction = dir.x > 0 ? 2 : 3;
                else
                    direction = dir.y > 0 ? 1 : 0;
                playerMovement.GetComponent<Animator>().SetInteger("Direction", direction);
            }

            var blockRb = grabbedBlock.GetComponent<Rigidbody2D>();
            blockRb.isKinematic = true;
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

        if (playerMovement != null)
        {
            playerMovement.freezeDirection = false;
        }
    }

    void UpdatePlayerSpeed()
    {
        if (playerMovement != null)
        {
            playerMovement.speed = isGrabbing ? grabSpeed : normalSpeed;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, grabRange);
    }
}
