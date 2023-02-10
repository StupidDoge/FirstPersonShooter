using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAttackController : MonoBehaviour
{
    public static Action<int, int> OnWeaponEquipped;
    public static Action<int> OnTotalAmmoAmountChanged;
    public static Action OnWeaponRemoved;

    [SerializeField] private Transform _itemContainer;
    [field: SerializeField] public int PistolAmmoAmount { get; private set; }
    [field: SerializeField] public int RifleAmmoAmount { get; private set; }
    [field: SerializeField] public int ShotgunAmmoAmount { get; private set; }

    [field: SerializeField] public int CurrentPistolAmmoAmount { get; private set; }
    [field: SerializeField] public int CurrentRifleAmmoAmount { get; private set; }
    [field: SerializeField] public int CurrentShotgunAmmoAmount { get; private set; }

    public bool ItemIsEquipped { get; private set; }

    private RangeWeaponPhysicalItem _equippedWeapon;
    private AmmoType _currentAmmoType;
    private Vector3 _holdPosition;
    private Vector3 _aimPosition;

    private int _currentWeaponTotalAmmo;

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
        PlayerInputHolder.OnReloadButtonPressed += StartReloading;
    }

    private void OnDisable()
    {
        Inventory.OnAmmoAmountChanged -= ChangeAmmoAmount;
        Inventory.OnActiveItemSet -= EquipItem;
        Inventory.OnActiveItemRemoved -= UnequipItem;
        PlayerInputHolder.OnReloadButtonPressed -= StartReloading;
    }

    private async void StartReloading()
    {
        if (_equippedWeapon != null)
            await _equippedWeapon.Reload();
    }

    private void Update()
    {
        if (!GlobalUIController.AnyUIPanelIsActive && _equippedWeapon != null)
        {
            if (_playerInputHolder.leftMouseClick)
            {
                if (_equippedWeapon.CanShoot && !_equippedWeapon.IsReloading && _equippedWeapon.TotalAmmo != 0)
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
                PistolAmmoAmount += amount;
                break;
            case AmmoType.Rifle:
                RifleAmmoAmount += amount;
                break;
            case AmmoType.Shotgun:
                ShotgunAmmoAmount += amount;
                break;
        }

        switch (_currentAmmoType)
        {
            case AmmoType.Pistol:
                _currentWeaponTotalAmmo = PistolAmmoAmount;
                break;
            case AmmoType.Rifle:
                _currentWeaponTotalAmmo = RifleAmmoAmount;
                break;
            case AmmoType.Shotgun:
                _currentWeaponTotalAmmo = ShotgunAmmoAmount;
                break;
        }

        if (_equippedWeapon != null)
        {
            _equippedWeapon.TotalAmmo = _currentWeaponTotalAmmo;
            OnTotalAmmoAmountChanged?.Invoke(_currentWeaponTotalAmmo - _equippedWeapon.CurrentAmmo);
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
            _currentAmmoType = weaponSO.WeaponAmmoType;

            switch (_currentAmmoType)
            {
                case AmmoType.Pistol:
                    _currentWeaponTotalAmmo = PistolAmmoAmount;
                    break;

                case AmmoType.Rifle:
                    _currentWeaponTotalAmmo = RifleAmmoAmount;
                    break;

                case AmmoType.Shotgun:
                    _currentWeaponTotalAmmo = ShotgunAmmoAmount;
                    break;
            }
            _equippedWeapon.TotalAmmo = _currentWeaponTotalAmmo;
            OnWeaponEquipped?.Invoke(_equippedWeapon.AmmoClip, _currentWeaponTotalAmmo);

            _holdPosition = weaponSO.HoldOffset;
            _aimPosition = weaponSO.AimPosition;
        }
    }

    private void UnequipItem()
    {
        Destroy(_itemContainer.GetComponentInChildren<PhysicalItemBase>().gameObject);
        OnWeaponRemoved?.Invoke();
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
