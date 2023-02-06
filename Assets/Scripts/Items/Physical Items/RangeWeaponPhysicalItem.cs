using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeaponPhysicalItem : PhysicalItemBase
{
    [SerializeField] private RangeWeaponSO _rangeWeaponSO;

    private int _ammoClip;
    private float _fireRate;

    public RangeWeaponSO WeaponTemplate => _rangeWeaponSO;
    public int AmmoClip => _ammoClip;

    private void Start()
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
}
