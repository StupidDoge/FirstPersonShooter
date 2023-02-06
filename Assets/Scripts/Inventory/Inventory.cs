using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    public static Action<ItemBase, int> OnItemAdded;
    public static Action<ItemBase, int> OnItemUpdated;
    public static Action<ItemBase> OnItemRemoved;

    public static readonly int INVENTORY_CAPACITY = 16;

    private Dictionary<ItemBase, int> _inventory = new(16);

    public Dictionary<ItemBase, int> InventoryDictionary => _inventory;

    private void OnEnable()
    {
        PhysicalItemBase.OnItemEquipped += Add;
        ItemContextMenu.OnItemDropped += Delete;
    }

    private void OnDisable()
    {
        PhysicalItemBase.OnItemEquipped -= Add;
        ItemContextMenu.OnItemDropped -= Delete;
    }

    private void Add(ItemBase item, int amount)
    {
        if (_inventory.Count >= INVENTORY_CAPACITY)
        {
            Debug.Log("Inventory is full");
            return;
        }

        if (_inventory.ContainsKey(item))
        {
            _inventory[item] += amount;
            OnItemUpdated?.Invoke(item, amount);
        }
        else
        {
            _inventory.Add(item, amount);
            OnItemAdded?.Invoke(item, amount);
        }
    }

    private void Delete(ItemBase item, int amount)
    {
        _inventory.Remove(item);
        OnItemRemoved?.Invoke(item);
    }
}
