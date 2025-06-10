using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    [SerializeField] private Sprite closedSprite;
    [SerializeField] private Sprite openSprite;

    public Collider2D transitionCollider;

    private SpriteRenderer spriteRenderer;
    private Collider2D col;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();

        // По умолчанию дверь закрыта
        Close();
    }

    public void Open()
    {
        if (spriteRenderer != null) spriteRenderer.sprite = openSprite;
        if (col != null) col.isTrigger = true;
        if (transitionCollider != null) transitionCollider.enabled = true;
    }

    public void Close()
    {
        if (spriteRenderer != null) spriteRenderer.sprite = closedSprite;
        if (col != null) col.isTrigger = false;
        if (transitionCollider != null) transitionCollider.enabled = false;
    }
}
