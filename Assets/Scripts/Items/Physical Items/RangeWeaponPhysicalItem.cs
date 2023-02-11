using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class RangeWeaponPhysicalItem : PhysicalItemBase
{
    public static Action<AmmoType> OnWeaponShot;
    public static Action<int, int> OnCurrentAmmoAmountChanged;

    [SerializeField] private RangeWeaponSO _rangeWeaponSO;

    private int _ammoClip;
    private float _fireRate;
    private float _reloadTime;
    private Camera _mainCamera;

    public RangeWeaponSO WeaponTemplate => _rangeWeaponSO;
    public int AmmoClip => _ammoClip;
    public float FireRate => _fireRate;

    public bool CanShoot { get; protected set; } = true;
    public bool IsReloading { get; protected set; } = false;
    public int TotalAmmo { get; set; }
    public int CurrentAmmo;

    private void Awake()
    {
        SetWeaponStats();
    }

    private void Start()
    {
        _mainCamera = Camera.main;
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

    public virtual async void Shoot()
    {
        if (CurrentAmmo == 0)
        {
            await Reload();
        }

        CanShoot = false;
        int milliseconds = (int)(_fireRate * 1000);

        Ray ray = _mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        ray.origin = transform.position;
        CurrentAmmo--;
        OnWeaponShot?.Invoke(WeaponTemplate.WeaponAmmoType);
        OnCurrentAmmoAmountChanged?.Invoke(CurrentAmmo, TotalAmmo);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log(_rangeWeaponSO.Name + " hit " + hit.collider.gameObject.name);
        }

        await Task.Delay(milliseconds);

        CanShoot = true;
    }

    public virtual async Task Reload()
    {
        IsReloading = true;
        int milliseconds = (int)(_reloadTime * 1000);
        int ammoToReload;
        if (TotalAmmo > AmmoClip)
        {
            ammoToReload = AmmoClip;
        } 
        else
        {
            ammoToReload = TotalAmmo;
        }

        await Task.Delay(milliseconds);

        if (TotalAmmo == 0)
            CurrentAmmo = 0;
        else 
            CurrentAmmo = ammoToReload;

        OnCurrentAmmoAmountChanged?.Invoke(CurrentAmmo, TotalAmmo);

        IsReloading = false;
    }
}
