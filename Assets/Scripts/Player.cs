using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Player: MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeReference] private float movingSpeed = 5f;
    [SerializeField] private float interactionDistance = 1.5f;
    [SerializeField] private LayerMask interactableLayer;

    private Rigidbody2D rb;
    private float minMovingSpeed = 0.1f;
    private bool isRunning = false;
    private InteractableObject currentInteractable;

    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CheckForInteractables();
        HandleInteraction();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVector();
        inputVector = inputVector.normalized;
        rb.MovePosition(rb.position + inputVector * (movingSpeed * Time.fixedDeltaTime));

        if (Mathf.Abs(inputVector.x) > minMovingSpeed || Mathf.Abs(inputVector.y) > minMovingSpeed)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        } 
    }

    private void CheckForInteractables()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactionDistance, interactableLayer);
        
        InteractableObject closestInteractable = null;
        float closestDistance = float.MaxValue;

        foreach (Collider2D collider in colliders)
        {
            InteractableObject interactable = collider.GetComponent<InteractableObject>();
            if (interactable != null)
            {
                float distance = Vector2.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestInteractable = interactable;
                }
            }
        }

        currentInteractable = closestInteractable;
    }

    private void HandleInteraction()
    {
        if (currentInteractable != null && Input.GetKeyDown(KeyCode.E))
        {
            currentInteractable.Interact();
        }
    }

    public bool IsRunning()
    {
        return isRunning;
    }
}

