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

    private void Add(ItemBase item, int amount, GameObject physicalItem)
    {
        if (_inventory.Count >= INVENTORY_CAPACITY)
        {
            Debug.Log("Inventory is full");
            return;
        }

        if (_inventory.ContainsKey(item))
        {
            if (item.Stackable)
            {
                _inventory[item] += amount;
                OnItemUpdated?.Invoke(item, amount);
                Destroy(physicalItem);
            }
            else
            {
                if (physicalItem.TryGetComponent(out RangeWeaponPhysicalItem weapon))
                {
                    Destroy(physicalItem);
                    AddAmmo(weapon);
                }
            }
        }
        else if (!_inventory.ContainsKey(item))
        {
            _inventory.Add(item, amount);
            OnItemAdded?.Invoke(item, amount);
            Destroy(physicalItem);
        }
    }

    private void Delete(ItemBase item, int amount)
    {
        _inventory.Remove(item);
        OnItemRemoved?.Invoke(item);
    }

    private void AddAmmo(RangeWeaponPhysicalItem weapon)
    {
        bool ammoFound = false;
        foreach(KeyValuePair<ItemBase, int> item in _inventory)
        {
            if (item.Key.GetType() == typeof(AmmoSO))
            {
                AmmoSO ammo = (AmmoSO)item.Key;
                if (ammo.Type == weapon.WeaponTemplate.WeaponAmmoType)
                {
                    ammoFound = true;
                    _inventory[item.Key] += weapon.AmmoClip;
                    OnItemUpdated?.Invoke(item.Key, weapon.AmmoClip);
                    return;
                }
            }
        }

        if (!ammoFound)
        {
            AmmoSO ammo = weapon.WeaponTemplate.AmmoBoxPrefab.AmmoTemplate;
            _inventory.Add(ammo, weapon.AmmoClip);
            OnItemAdded?.Invoke(ammo, weapon.AmmoClip);
        }
    }
}
