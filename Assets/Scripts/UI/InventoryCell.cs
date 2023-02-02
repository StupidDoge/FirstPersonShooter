using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryCell : MonoBehaviour, IDropHandler
{
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
        DragAndDrop draggableItem = dropped.GetComponent<DragAndDrop>();
        draggableItem.ParentAfterDrag = transform;
    }
}
