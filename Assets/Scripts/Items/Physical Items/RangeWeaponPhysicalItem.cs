using System;
using System.Collections;
using UnityEngine;

public class RangeWeaponPhysicalItem : PhysicalWeaponItem, IAimable, IReloadable
{
    public static Action<int, int> OnWeaponEquipped;
    public static Action OnWeaponUnequipped;
    public static Action<AmmoType> OnWeaponShot;
    public static Action<int, int> OnCurrentAmmoAmountChanged;
    public static Func<AmmoType, int> OnRangeWeaponEquipped;

    [SerializeField] private RangeWeaponSO _rangeWeaponSO;

    private int _ammoClip;
    private float _fireRate;
    private float _reloadTime;
    private Camera _mainCamera;

    public RangeWeaponSO WeaponTemplate => _rangeWeaponSO;
    public int AmmoClip => _ammoClip;
    public float FireRate => _fireRate;
    public Camera MainCamera => _mainCamera;

    public bool CanShoot { get; protected set; } = true;
    public bool IsReloading { get; protected set; } = false;
    [field: SerializeField] public int TotalAmmo { get; set; }
    public int CurrentAmmo;

    private void Awake()
    {
        SetWeaponStats();
    }

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        Inventory.OnAmmoAmountChanged += UpdateTotalAmmoAmount;
    }

    private void OnDisable()
    {
        Inventory.OnAmmoAmountChanged -= UpdateTotalAmmoAmount;
    }

    private void SetWeaponStats()
    {
        _ammoClip = _rangeWeaponSO.AmmoClip;
        _fireRate = _rangeWeaponSO.FireRate;
        _reloadTime = _rangeWeaponSO.ReloadTime;
    }

    public override void Interact(Interactor interactor)
    {
        OnItemEquipped?.Invoke(_rangeWeaponSO, baseAmount, gameObject);
        OnItemEquipped?.Invoke(_rangeWeaponSO.AmmoBoxPrefab.AmmoTemplate, CurrentAmmo, gameObject);
    }

    public override void Equip()
    {
        base.Equip();
        transform.localPosition = _rangeWeaponSO.HoldOffset;
        TotalAmmo = (int)OnRangeWeaponEquipped?.Invoke(_rangeWeaponSO.WeaponAmmoType);
        OnWeaponEquipped?.Invoke(CurrentAmmo, TotalAmmo);
    }

    public override void Unequip()
    {
        base.Unequip();
        OnWeaponUnequipped?.Invoke();
    }

    public IEnumerator Reload()
    {
        IsReloading = true;

        int ammoToReload;
        if (TotalAmmo > AmmoClip)
            ammoToReload = AmmoClip;
        else
            ammoToReload = TotalAmmo;

        if (TotalAmmo == 0)
            CurrentAmmo = 0;
        else
            CurrentAmmo = ammoToReload;

        yield return new WaitForSeconds(_reloadTime);
        OnCurrentAmmoAmountChanged?.Invoke(CurrentAmmo, TotalAmmo);
        IsReloading = false;
    }

    public void Aim(bool aimInput)
    {
        if (aimInput)
            transform.localPosition = WeaponTemplate.AimPosition;
        else
            transform.localPosition = WeaponTemplate.HoldOffset;
    }

    protected IEnumerator AttackCoroutine()
    {
        CanAttack = false;
        yield return new WaitForSeconds(FireRate);
        CanAttack = true;
    }

    private void UpdateTotalAmmoAmount(AmmoType ammoType, int amount)
    {
        if (_rangeWeaponSO.WeaponAmmoType == ammoType)
        {
            TotalAmmo += amount;
            OnCurrentAmmoAmountChanged?.Invoke(CurrentAmmo, TotalAmmo);
        }
    }
}
