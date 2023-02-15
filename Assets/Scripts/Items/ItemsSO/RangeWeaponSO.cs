using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Range Weapon", menuName = "Inventory System/Items/Weapons/Range Weapon")]
public class RangeWeaponSO : WeaponSO
{
    [SerializeField] private Vector3 _aimPosition;
    [SerializeField] private int _ammoClip;
    [SerializeField] private float _fireRate;
    [SerializeField] private float _reloadTime;
    [SerializeField] private RangeWeaponType _type;
    [SerializeField] private AmmoType _ammoType;
    [SerializeField] private AmmoBoxPhysicalItem _ammoBoxPrefab;

    public Vector3 AimPosition => _aimPosition;
    public int AmmoClip => _ammoClip;
    public float FireRate => _fireRate;
    public float ReloadTime => _reloadTime;
    public RangeWeaponType Type => _type;
    public AmmoType WeaponAmmoType => _ammoType;
    public AmmoBoxPhysicalItem AmmoBoxPrefab => _ammoBoxPrefab;
}

public enum RangeWeaponType
{
    Classic = 1,
    Shotgun = 2
}