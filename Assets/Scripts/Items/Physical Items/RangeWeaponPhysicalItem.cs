using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeaponPhysicalItem : PhysicalItemBase
{
    [SerializeField] private RangeWeaponSO _rangeWeaponSO;

    private float _ammoClip;
    private float _fireRate;

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
        OnItemEquipped?.Invoke(_rangeWeaponSO, baseAmount);
        Destroy(gameObject);
    }
}
