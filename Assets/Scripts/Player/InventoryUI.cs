using UnityEngine;
using UnityEngine.UI;
using Cainos.CustomizablePixelCharacter;

[System.Serializable]
public class SlotSprites
{
    public Sprite normalSprite;
    public Sprite selectedSprite;
}

public enum ItemType
{
    None,
    MeleeWeapon,
    Bow,
    Food
}

[System.Serializable]
public class InventoryItem
{
    public ItemType type;
    public Sprite icon;
    public GameObject prefab;
    public PixelCharacterController.AttackActionType attackActionType;

    public InventoryItem(ItemType type, Sprite icon, GameObject prefab, PixelCharacterController.AttackActionType attackActionType)
    {
        this.type = type;
        this.icon = icon;
        this.prefab = prefab;
        this.attackActionType = attackActionType;
    }
}

public class InventoryUI : MonoBehaviour
{
    public Image[] slots;
    public Image[] iconImages;
    public SlotSprites[] slotSprites;
    public int selectedSlot = 0;
    public PixelCharacterController characterController;

    private InventoryItem[] items = new InventoryItem[3];

    private static InventoryUI instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        SelectSlot(0);
        for (int i = 0; i < items.Length; i++)
            items[i] = null;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SelectSlot(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SelectSlot(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SelectSlot(2);
    }

    public void OnSlotClick(int slotIndex)
    {
        SelectSlot(slotIndex);
    }

    public void SelectSlot(int slotIndex)
    {
        selectedSlot = slotIndex;
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].sprite = (i == slotIndex) ? slotSprites[i].selectedSprite : slotSprites[i].normalSprite;

            if (items[i] != null)
            {
                iconImages[i].sprite = items[i].icon;
                iconImages[i].enabled = true;
            }
            else
            {
                iconImages[i].sprite = null;
                iconImages[i].enabled = false;
            }
        }

        if (characterController != null)
        {
            var character = characterController.GetComponent<PixelCharacter>();
            var item = items[slotIndex];
            if (item != null && item.prefab != null)
            {
                character.AddWeapon(item.prefab, true);
                characterController.attackAction = item.attackActionType;
            }
            else
            {
                character.ClearWeapon();
            }
        }
    }

    public bool AddItemToSlot(int slotIndex, InventoryItem item)
    {
        if (slotIndex == 2 && item.type != ItemType.Food)
        {
            Debug.Log("В третий слот можно положить только еду!");
            return false;
        }
        items[slotIndex] = item;
        UpdateSlotUI(slotIndex);

        if (slotIndex == selectedSlot)
        {
            SelectSlot(selectedSlot);
        }

        return true;
    }

    public void ClearSlot(int slotIndex)
    {
        items[slotIndex] = null;
        UpdateSlotUI(slotIndex);
    }

    public InventoryItem GetSelectedItem()
    {
        return items[selectedSlot];
    }

    private void UpdateSlotUI(int slotIndex)
    {
        if (items[slotIndex] != null)
        {
            iconImages[slotIndex].sprite = items[slotIndex].icon;
            iconImages[slotIndex].enabled = true;
        }
        else
        {
            iconImages[slotIndex].sprite = null;
            iconImages[slotIndex].enabled = false;
        }
    }

    public InventoryItem GetItem(int slotIndex) => items[slotIndex];

    public void SwapItems(int from, int to)
    {
        var temp = items[from];
        items[from] = items[to];
        items[to] = temp;
        UpdateSlotUI(from);
        UpdateSlotUI(to);
        // Если один из слотов активный — обновить экипировку
        if (from == selectedSlot || to == selectedSlot)
            SelectSlot(selectedSlot);
    }
}