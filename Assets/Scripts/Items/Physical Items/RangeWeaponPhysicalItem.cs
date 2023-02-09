using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class RangeWeaponPhysicalItem : PhysicalItemBase
{
    [SerializeField] private RangeWeaponSO _rangeWeaponSO;

    private int _ammoClip;
    private float _fireRate;

    public RangeWeaponSO WeaponTemplate => _rangeWeaponSO;
    public int AmmoClip => _ammoClip;
    public float FireRate => _fireRate;

    public bool CanShoot { get; private set; } = true;

    private void Awake()
    {
        SetWeaponStats();
    }

    private void SetWeaponStats()
    {
        _ammoClip = _rangeWeaponSO.AmmoClip;
        _fireRate = _rangeWeaponSO.FireRate;
    }

    public override void Interact(Interactor interactor)
    {
        OnItemEquipped?.Invoke(_rangeWeaponSO, baseAmount, gameObject);
    }

    public async void Shoot()
    {
        CanShoot = false;
        int milliseconds = (int)(_fireRate * 1000);

        Debug.Log(_rangeWeaponSO.Name + " shooting");

        await Task.Delay(milliseconds);

        CanShoot = true;
    }
}
