using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    private Animator animator;

    private const string IS_RUNNING = "IsRunning";

    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
       animator.SetBool(IS_RUNNING, Player.Instance.IsRunning());

        Vector2 movement = GameInput.Instance.GetMovementVector();

        if (movement.x > 0.01f)
        {
            spriteRenderer.flipX = false; 
        }
        else if (movement.x < -0.01f)
        {
            spriteRenderer.flipX = true;
        }
    }
}
