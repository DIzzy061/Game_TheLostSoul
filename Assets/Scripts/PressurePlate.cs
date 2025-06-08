using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private Door door;
    [SerializeField] private Sprite pressedSprite;
    [SerializeField] private Sprite unpressedSprite;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Box"))
        {
            spriteRenderer.sprite = pressedSprite;
            door.Open();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Box"))
        {
            spriteRenderer.sprite = unpressedSprite;
            door.Close();
        }
    }
}

