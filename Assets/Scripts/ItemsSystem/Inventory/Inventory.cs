using System.Collections.Generic;
using UnityEngine;
using System;

namespace ItemsSystem
{
    public class Inventory : MonoBehaviour
    {
        public event Action<ItemBase, int, GameObject> OnItemAdded;
        public event Action<ItemBase, int> OnItemUpdated;
        public event Action<ItemBase> OnItemRemoved;
        public event Action<ItemBase, InventoryItem> OnActiveItemSet;
        public event Func<int> OnActiveItemRemoved;
        public static event Action<AmmoType, int> OnAmmoAmountChanged;
        public event Action OnItemFromActiveSlotAdded;

        public static readonly int INVENTORY_CAPACITY = 16;

        private readonly Dictionary<ItemBase, int> _inventory = new();
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
            RangeWeaponPhysicalItem.OnRangeWeaponEquipped += SetAmmo;
        }

        private void OnDisable()
        {
            PhysicalItemBase.OnItemEquipped -= Add;
            ItemContextMenu.OnItemDropped -= Remove;
            ItemContextMenu.OnItemEquipped -= SetActiveItem;
            InventoryCell.OnItemUnequipped -= RemoveActiveItem;
            RangeWeaponPhysicalItem.OnWeaponShot -= DecreaseAmmo;
            RangeWeaponPhysicalItem.OnRangeWeaponEquipped -= SetAmmo;
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
            }

            Destroy(physicalItem);
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

        private int RemoveActiveItem()
        {
            AddFromActive(_activeItem);
            _activeItem = null;
            int ammo = (int)OnActiveItemRemoved?.Invoke();
            return ammo;
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

        private int SetAmmo(AmmoType ammoType)
        {
            int currentAmmo = 0;
            foreach (KeyValuePair<ItemBase, int> item in _inventory)
            {
                if (item.Key.GetType() == typeof(AmmoSO))
                {
                    AmmoSO ammo = (AmmoSO)item.Key;
                    if (ammo.Type == ammoType)
                    {
                        currentAmmo = item.Value;
                    }
                }
            }

            return currentAmmo;
        }
    }
}
