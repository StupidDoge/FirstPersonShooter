using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Dictionary<ItemBase, int> _inventory = new();

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
        if (_inventory.ContainsKey(item))
            _inventory[item] += amount;
        else
            _inventory.Add(item, amount);

        foreach (KeyValuePair<ItemBase, int> cell in _inventory)
            Debug.Log(cell.Key.Name + " equipped, amount = " + cell.Value);
    }
}
