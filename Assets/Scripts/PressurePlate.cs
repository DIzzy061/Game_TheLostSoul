using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private Door2DTopDown door;
    [SerializeField] private Sprite pressedSprite;
    [SerializeField] private Sprite unpressedSprite;

    private SpriteRenderer spriteRenderer;
    private int objectsOnPlate = 0;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = unpressedSprite;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Box") || other.CompareTag("Player"))
        {
            objectsOnPlate++;

            if (objectsOnPlate == 1)
            {
                spriteRenderer.sprite = pressedSprite;
                door.Open();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Box") || other.CompareTag("Player"))
        {
            objectsOnPlate--;

            if (objectsOnPlate <= 0)
            {
                spriteRenderer.sprite = unpressedSprite;
                door.Close();
            }
        }
    }
}