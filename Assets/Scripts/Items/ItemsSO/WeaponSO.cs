using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSO : ItemBase
{
    [SerializeField] private WeaponType _weaponType;
    [SerializeField] private Vector3 _holdOffset;
    [SerializeField] private Quaternion _holdRotation;

    public WeaponType Type => _weaponType;
    public Vector3 HoldOffset => _holdOffset;
    public Quaternion HoldRotation => _holdRotation;
}

public enum WeaponType
{
    Melee,
    Range,
    Throwable
}
