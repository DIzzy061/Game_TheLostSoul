using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class YSort : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    [SerializeField] private int sortingOffset = 0;
    [SerializeField] private bool runEveryFrame = true;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateSorting();
    }

    void Update()
    {
        if (runEveryFrame)
            UpdateSorting();
    }

    public void UpdateSorting()
    {
        int baseOrder = 10000; 
        spriteRenderer.sortingOrder = baseOrder + Mathf.RoundToInt(-transform.position.y * 100) + sortingOffset;
    }
}
