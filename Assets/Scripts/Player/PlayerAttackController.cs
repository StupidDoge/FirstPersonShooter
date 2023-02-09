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

    private RangeWeaponPhysicalItem _equippedWeapon;
    private Vector3 _holdPosition;
    private Vector3 _aimPosition;

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
        if (!GlobalUIController.AnyUIPanelIsActive && _equippedWeapon != null)
        {
            if (_playerInputHolder.leftMouseClick)
            {
                if (_equippedWeapon.CanShoot)
                    _equippedWeapon.Shoot();
            }

            Aim();
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
            _equippedWeapon = item.GetComponent<RangeWeaponPhysicalItem>();

            _holdPosition = weaponSO.HoldOffset;
            _aimPosition = weaponSO.AimPosition;
        }
    }

    private void UnequipItem()
    {
        Destroy(_itemContainer.GetComponentInChildren<PhysicalItemBase>().gameObject);
        ItemIsEquipped = false;
        _equippedWeapon = null;
    }

    private void Aim()
    {
        if (_playerInputHolder.rightMouseClick)
            _equippedWeapon.transform.localPosition = _aimPosition;
        else
            _equippedWeapon.transform.localPosition = _holdPosition;
    }
}
