using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    [SerializeField] private Sprite closedSprite;
    [SerializeField] private Sprite openSprite;

    private SpriteRenderer spriteRenderer;
    private Collider2D col;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    public void Open()
    {
        Debug.Log("Дверь открыта");
        spriteRenderer.sprite = openSprite;
        col.isTrigger = true; 
    }

    public void Close()
    {
        spriteRenderer.sprite = closedSprite;
        col.isTrigger = false;
    }
}
