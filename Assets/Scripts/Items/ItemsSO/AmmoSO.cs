using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ammo", menuName = "Inventory System/Items/Ammo")]
public class AmmoSO : ItemBase
{
    [SerializeField] private AmmoType _type;
    [SerializeField] private float _damage;

    public float Damage => _damage;
    public AmmoType Type => _type;
}

public enum AmmoType
{
    Pistol,
    Rifle,
    Shotgun
}
