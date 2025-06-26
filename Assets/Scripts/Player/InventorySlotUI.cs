using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public int slotIndex;
    public InventoryUI inventoryUI;
    public Image iconImage;

    private static InventorySlotUI draggingSlot;
    private static GameObject dragIconObj;
    private static bool droppedOnSlot;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (inventoryUI.GetItem(slotIndex) == null) return;
        draggingSlot = this;
        droppedOnSlot = false;

        // Создаём визуальный drag-объект
        dragIconObj = new GameObject("DragIcon");
        var canvas = inventoryUI.GetComponentInParent<Canvas>();
        dragIconObj.transform.SetParent(canvas.transform, false);
        var img = dragIconObj.AddComponent<Image>();
        img.sprite = iconImage.sprite;
        img.raycastTarget = false;
        img.SetNativeSize();
        var rect = dragIconObj.GetComponent<RectTransform>();
        rect.sizeDelta = img.sprite.rect.size;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (dragIconObj != null)
        {
            var canvas = inventoryUI.GetComponentInParent<Canvas>();
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                Input.mousePosition,
                canvas.worldCamera,
                out Vector2 localPoint
            );
            dragIconObj.GetComponent<RectTransform>().localPosition = localPoint;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (dragIconObj != null)
            Destroy(dragIconObj);
        dragIconObj = null;

        // Если не было дропа на слот — удалить предмет
        if (!droppedOnSlot && draggingSlot != null)
        {
            inventoryUI.ClearSlot(draggingSlot.slotIndex);
        }
        draggingSlot = null;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (draggingSlot != null && draggingSlot != this)
        {
            inventoryUI.SwapItems(draggingSlot.slotIndex, slotIndex);
            droppedOnSlot = true;
        }
    }
} 