using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSO : ItemBase
{
    [SerializeField] private WeaponType _weaponType;
    [SerializeField] private Vector3 _holdOffset;
    [SerializeField] private Vector3 _aimPosition;

    public WeaponType Type => _weaponType;
    public Vector3 HoldOffset => _holdOffset;
    public Vector3 AimPosition => _aimPosition;
}

public enum WeaponType
{
    Melee,
    Range,
    Throwable
}
