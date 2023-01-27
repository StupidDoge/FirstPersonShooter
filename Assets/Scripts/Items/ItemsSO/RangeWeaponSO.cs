using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Range Weapon", menuName = "Inventory System/Items/Weapons/Range Weapon")]
public class RangeWeaponSO : WeaponSO
{
    [SerializeField] private float _ammoClip;
    [SerializeField] private float _fireRate;

    public float AmmoClip => _ammoClip;
    public float FireRate => _fireRate;
}
