using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    public static Action<ItemBase, int> OnItemAdded;
    public static Action<ItemBase, int> OnItemUpdated;
    public static Action<ItemBase> OnItemRemoved;
    public static Action<ItemBase> OnActiveItemSet;
    public static Action OnActiveItemRemoved;
    public static Action<AmmoType, int> OnAmmoAmountChanged;
    public static Action OnItemFromActiveSlotAdded;

    public static readonly int INVENTORY_CAPACITY = 16;

    private Dictionary<ItemBase, int> _inventory = new(16);
    private ItemBase _activeItem;

    public Dictionary<ItemBase, int> InventoryDictionary => _inventory;

    private void OnEnable()
    {
        PhysicalItemBase.OnItemEquipped += Add;
        ItemContextMenu.OnItemDropped += Delete;
        ItemContextMenu.OnItemEquipped += SetActiveItem;
        InventoryCell.OnItemUnequipped += RemoveActiveItem;
    }

    private void OnDisable()
    {
        PhysicalItemBase.OnItemEquipped -= Add;
        ItemContextMenu.OnItemDropped -= Delete;
        ItemContextMenu.OnItemEquipped -= SetActiveItem;
        InventoryCell.OnItemUnequipped -= RemoveActiveItem;
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

        SearchForAmmo();
    }

    private void AddFromActive(ItemBase item)
    {
        if (_inventory.Count >= INVENTORY_CAPACITY)
        {
            Debug.Log("Inventory is full");
            return;
        }

        _inventory.Add(item, 1);
        OnItemFromActiveSlotAdded?.Invoke();
    }

    private void Delete(ItemBase item, int amount)
    {
        _inventory.Remove(item);
        OnItemRemoved?.Invoke(item);

        if (item.GetType() == typeof(AmmoSO))
        {
            AmmoSO ammo = (AmmoSO)item;
            OnAmmoAmountChanged(ammo.Type, 0);
        }
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

    private void SearchForAmmo()
    {
        foreach (KeyValuePair<ItemBase, int> item in _inventory)
        {
            if (item.Key.GetType() == typeof(AmmoSO))
            {
                AmmoSO ammo = (AmmoSO)item.Key;
                OnAmmoAmountChanged?.Invoke(ammo.Type, item.Value);
            }
        }
    }

    private void SetActiveItem(ItemBase item)
    {
        if (_activeItem != null)
            return;

        _activeItem = item;
        OnActiveItemSet?.Invoke(item);
        Delete(item, 1);
    }

    private void RemoveActiveItem()
    {
        AddFromActive(_activeItem);
        _activeItem = null;
        OnActiveItemRemoved?.Invoke();

        /*foreach (KeyValuePair<ItemBase, int> i in _inventory)
        {
            Debug.Log(i.Key.Name);
        }*/
    }
}
