using UnityEngine;

public class Door : InteractableObject
{
    [SerializeField] private Sprite closedSprite;
    [SerializeField] private Sprite openSprite;
    public Collider2D transitionCollider;

    private SpriteRenderer spriteRenderer;
    private Collider2D col;
    private bool isOpen = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        Close();
    }

    protected override void OnInteract()
    {
        if (isOpen)
            Close();
        else
            Open();
    }

    public void Open()
    {
        isOpen = true;
        if (spriteRenderer != null) spriteRenderer.sprite = openSprite;
        if (col != null) col.isTrigger = true;
        if (transitionCollider != null) transitionCollider.enabled = true;
    }

    public void Close()
    {
        isOpen = false;
        if (spriteRenderer != null) spriteRenderer.sprite = closedSprite;
        if (col != null) col.isTrigger = false;
        if (transitionCollider != null) transitionCollider.enabled = false;
    }
}
