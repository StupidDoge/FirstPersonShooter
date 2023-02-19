using UnityEngine;
using UnityEngine.EventSystems;

namespace ItemsSystem
{
    public class ActiveItemCell : MonoBehaviour, IDropHandler
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
            if (transform.childCount != 0 || eventData.pointerDrag == null || 
                !eventData.pointerDrag.GetComponent<InventoryItem>().ItemSO.Equippable)
                return;

            GameObject dropped = eventData.pointerDrag;
            DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();
            InventoryItem inventoryItem = dropped.GetComponent<InventoryItem>();
            inventoryItem.CellNumber = Id;
            inventoryItem.IsEquipped = true;
            draggableItem.ParentAfterDrag = transform;
            ItemContextMenu.OnItemEquipped?.Invoke(inventoryItem);
        }
    }
}
