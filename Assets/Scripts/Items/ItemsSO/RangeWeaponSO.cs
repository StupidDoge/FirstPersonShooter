using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Range Weapon", menuName = "Inventory System/Items/Weapons/Range Weapon")]
public class RangeWeaponSO : WeaponSO
{
    [SerializeField] private int _ammoClip;
    [SerializeField] private float _fireRate;
    [SerializeField] private AmmoType _ammoType;
    [SerializeField] private AmmoBoxPhysicalItem _ammoBoxPrefab;

    public int AmmoClip => _ammoClip;
    public float FireRate => _fireRate;
    public AmmoType WeaponAmmoType => _ammoType;
    public AmmoBoxPhysicalItem AmmoBoxPrefab => _ammoBoxPrefab;
}
