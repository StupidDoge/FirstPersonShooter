using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryCell : MonoBehaviour, IDropHandler
{
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
    }
}
