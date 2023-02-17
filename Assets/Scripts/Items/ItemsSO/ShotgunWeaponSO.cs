using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Shotgun Weapon", menuName = "Inventory System/Items/Weapons/Shotgun Weapon")]
public class ShotgunWeaponSO : RangeWeaponSO
{
    [SerializeField] private int _amountOfRays;

    public int AmountOfRays => _amountOfRays;
}
