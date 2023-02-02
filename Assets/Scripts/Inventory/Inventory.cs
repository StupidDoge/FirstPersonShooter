using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static readonly int INVENTORY_CAPACITY = 16;

    private Dictionary<ItemBase, int> _inventory = new(16);

    public Dictionary<ItemBase, int> InventoryDictionary => _inventory;

    private void OnEnable()
    {
        PhysicalItemBase.OnItemEquipped += Add;
    }

    private void OnDisable()
    {
        PhysicalItemBase.OnItemEquipped -= Add;
    }

    private void Add(ItemBase item, int amount)
    {
        if (_inventory.Count >= INVENTORY_CAPACITY)
        {
            Debug.Log("Inventory is full");
            return;
        }

        if (_inventory.ContainsKey(item))
            _inventory[item] += amount;
        else
            _inventory.Add(item, amount);

        foreach (KeyValuePair<ItemBase, int> cell in _inventory)
            Debug.Log(cell.Key.Name + " equipped, amount = " + cell.Value);
    }
}
