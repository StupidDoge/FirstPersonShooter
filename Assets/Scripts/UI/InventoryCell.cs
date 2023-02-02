using UnityEngine;

public class InventoryCell : MonoBehaviour
{
    public InventoryItem Item { get; set; }

    public void DeleteItem()
    {
        if (Item != null)
            Destroy(Item.gameObject);
    }
}
