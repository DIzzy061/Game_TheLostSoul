using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemType type;
    public Sprite icon;
    public GameObject prefab;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            InventoryUI inventory = FindObjectOfType<InventoryUI>();
            if (inventory != null)
            {
                var actionType =
                    type == ItemType.Bow ? Cainos.CustomizablePixelCharacter.PixelCharacterController.AttackActionType.Archery :
                    type == ItemType.MeleeWeapon ? Cainos.CustomizablePixelCharacter.PixelCharacterController.AttackActionType.Swipe :
                    Cainos.CustomizablePixelCharacter.PixelCharacterController.AttackActionType.None;

                InventoryItem item = new InventoryItem(type, icon, prefab, actionType);

                bool added = false;
                if (type == ItemType.Food)
                {
                    added = inventory.AddItemToSlot(2, item);
                }
                else
                {
                    for (int i = 0; i < 2; i++)
                    {
                        if (inventory.AddItemToSlot(i, item))
                        {
                            added = true;
                            break;
                        }
                    }
                }

                if (added)
                {
                    Destroy(gameObject);
                }
                else
                {
                    Debug.Log("Нет свободного слота для этого предмета!");
                }
            }
        }
    }
} 