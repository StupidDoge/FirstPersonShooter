using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    [SerializeField] private Transform _itemContainer;
    [field: SerializeField] public int PistolAmmoAmount { get; private set; }
    [field: SerializeField] public int RifleAmmoAmount { get; private set; }
    [field: SerializeField] public int ShotgunAmmoAmount { get; private set; }

    public bool ItemIsEquipped { get; private set; }

    [SerializeField] private RangeWeaponPhysicalItem _equippedWeapon;
    [SerializeField] private float _currentWeaponFireRate;
    private float _time;

    private PlayerInputHolder _playerInputHolder;

    private void Start()
    {
        _playerInputHolder = GetComponent<PlayerInputHolder>();
    }

    private void OnEnable()
    {
        Inventory.OnAmmoAmountChanged += ChangeAmmoAmount;
        Inventory.OnActiveItemSet += EquipItem;
        Inventory.OnActiveItemRemoved += UnequipItem;
    }

    private void OnDisable()
    {
        Inventory.OnAmmoAmountChanged -= ChangeAmmoAmount;
        Inventory.OnActiveItemSet -= EquipItem;
        Inventory.OnActiveItemRemoved -= UnequipItem;
    }

    private void Update()
    {
        _time += Time.deltaTime;
        if (!GlobalUIController.AnyUIPanelIsActive && _equippedWeapon != null)
        {
            if (_playerInputHolder.rightMouseClick)
                Debug.Log("right click");
            if (_playerInputHolder.leftMouseClick)
            {
                if (_time >= _currentWeaponFireRate)
                {
                    _time = 0.0f;
                    _equippedWeapon.Shoot();
                }
            }
        }
    }

    private void ChangeAmmoAmount(AmmoType ammoType, int amount)
    {
        switch (ammoType)
        {
            case AmmoType.Pistol:
                PistolAmmoAmount = amount;
                break;
            case AmmoType.Rifle:
                RifleAmmoAmount = amount;
                break;
            case AmmoType.Shotgun:
                ShotgunAmmoAmount = amount;
                break;
        }
    }

    private void EquipItem(ItemBase itemSO)
    {
        if (ItemIsEquipped)
            return;

        if (itemSO.GetType() == typeof(RangeWeaponSO))
        {
            RangeWeaponSO weaponSO = (RangeWeaponSO)itemSO;
            GameObject item = Instantiate(weaponSO.ItemPrefab, _itemContainer);
            item.transform.localPosition = weaponSO.HoldOffset;
            item.transform.localRotation = Quaternion.identity;
            item.GetComponent<Rigidbody>().isKinematic = true;
            item.GetComponent<BoxCollider>().enabled = false;
            ItemIsEquipped = true;
            _equippedWeapon = weaponSO.ItemPrefab.GetComponent<RangeWeaponPhysicalItem>();
            _currentWeaponFireRate = weaponSO.FireRate;
        }
    }

    private void UnequipItem()
    {
        Destroy(_itemContainer.GetComponentInChildren<PhysicalItemBase>().gameObject);
        ItemIsEquipped = false;
        _equippedWeapon = null;
    }
}
