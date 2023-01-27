using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSO : ItemBase
{
    [SerializeField] private WeaponType _weaponType;

    public WeaponType Type => _weaponType;
}

public enum WeaponType
{
    Melee,
    Range,
    Throwable
}
