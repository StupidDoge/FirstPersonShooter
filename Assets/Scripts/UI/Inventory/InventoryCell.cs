using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class InventoryCell : MonoBehaviour, IDropHandler
{
    public static event Func <int> OnItemUnequipped;

    [field: SerializeField] public int Id { get; set; }
    public InventoryItem Item { get; set; }

    public void DeleteItem()
    {
        if (Item != null)
            Destroy(Item.gameObject);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount != 0 || eventData.pointerDrag == null)
            return;

        GameObject dropped = eventData.pointerDrag;
        DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();
        InventoryItem inventoryItem = dropped.GetComponent<InventoryItem>();
        inventoryItem.CellNumber = Id;
        draggableItem.ParentAfterDrag = transform;

        if (inventoryItem.IsEquipped)
        {
            inventoryItem.IsEquipped = false;
            inventoryItem.WeaponCurrentAmmoAmount = (int)OnItemUnequipped?.Invoke();
            return;
        }
    }
}
