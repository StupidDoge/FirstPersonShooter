using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Inventory : MonoBehaviour
{
    public static Action<ItemBase, int, GameObject> OnItemAdded;
    public static Action<ItemBase, int> OnItemUpdated;
    public static Action<ItemBase> OnItemRemoved;
    public static Action<ItemBase, InventoryItem> OnActiveItemSet;
    public static Action OnActiveItemRemoved;
    public static Action<AmmoType, int> OnAmmoAmountChanged;
    public static Action OnItemFromActiveSlotAdded;

    public static readonly int INVENTORY_CAPACITY = 16;

    private Dictionary<ItemBase, int> _inventory = new(16);
    private ItemBase _activeItem;

    public Dictionary<ItemBase, int> InventoryDictionary => _inventory;
    public ItemBase ActiveItem => _activeItem;

    private void OnEnable()
    {
        PhysicalItemBase.OnItemEquipped += Add;
        ItemContextMenu.OnItemDropped += Remove;
        ItemContextMenu.OnItemEquipped += SetActiveItem;
        InventoryCell.OnItemUnequipped += RemoveActiveItem;
        RangeWeaponPhysicalItem.OnWeaponShot += DecreaseAmmo;
    }

    private void OnDisable()
    {
        PhysicalItemBase.OnItemEquipped -= Add;
        ItemContextMenu.OnItemDropped -= Remove;
        ItemContextMenu.OnItemEquipped -= SetActiveItem;
        InventoryCell.OnItemUnequipped -= RemoveActiveItem;
        RangeWeaponPhysicalItem.OnWeaponShot -= DecreaseAmmo;
    }

    private void Add(ItemBase item, int amount, GameObject physicalItem)
    {
        if (_inventory.Count >= INVENTORY_CAPACITY)
        {
            Debug.Log("Inventory is full");
            return;
        }

        if (_inventory.ContainsKey(item) || _activeItem == item)
        {
            if (item.Stackable)
            {
                _inventory[item] += amount;
                OnItemUpdated?.Invoke(item, amount);
                if (item.GetType() == typeof(AmmoSO))
                {
                    AmmoSO ammoSO = (AmmoSO)item;
                    OnAmmoAmountChanged?.Invoke(ammoSO.Type, amount);
                }
                Destroy(physicalItem);
            }
            else
            {
                if (physicalItem.TryGetComponent(out RangeWeaponPhysicalItem weapon))
                {
                    Destroy(physicalItem);
                }
            }
        }
        else if (!_inventory.ContainsKey(item) && ActiveItem != item && amount != 0)
        {
            _inventory.Add(item, amount);
            OnItemAdded?.Invoke(item, amount, physicalItem);
            if (item.GetType() == typeof(AmmoSO))
            {
                AmmoSO ammoSO = (AmmoSO)item;
                OnAmmoAmountChanged?.Invoke(ammoSO.Type, amount);
            }
            Destroy(physicalItem);
        }
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

    private void Remove(ItemBase item, int amount)
    {
        _inventory.Remove(item);
        OnItemRemoved?.Invoke(item);

        if (item.GetType() == typeof(AmmoSO))
        {
            AmmoSO ammo = (AmmoSO)item;
            OnAmmoAmountChanged?.Invoke(ammo.Type, -amount);
        }
    }

    private void SetActiveItem(InventoryItem item)
    {
        if (_activeItem != null)
            return;

        _activeItem = item.ItemSO;
        OnActiveItemSet?.Invoke(item.ItemSO, item);
        Remove(item.ItemSO, 1);
    }

    private void RemoveActiveItem()
    {
        AddFromActive(_activeItem);
        _activeItem = null;
        OnActiveItemRemoved?.Invoke();
    }

    private void DecreaseAmmo(AmmoType ammoType)
    {
        foreach (KeyValuePair<ItemBase, int> item in _inventory)
        {
            if (item.Key.GetType() == typeof(AmmoSO))
            {
                AmmoSO ammo = (AmmoSO)item.Key;
                if (ammo.Type == ammoType)
                {
                    _inventory[item.Key]--;
                    OnAmmoAmountChanged?.Invoke(ammoType, -1);
                    OnItemUpdated?.Invoke(item.Key, -1);

                    if (_inventory[item.Key] == 0)
                    {
                        Remove(item.Key, 0);
                    }

                    return;
                }
            }
        }
    }
}
