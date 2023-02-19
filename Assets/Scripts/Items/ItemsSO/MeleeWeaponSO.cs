using UnityEngine;

[CreateAssetMenu(fileName = "New Melee Weapon", menuName = "Inventory System/Items/Weapons/Melee Weapon")]
public class MeleeWeaponSO : WeaponSO
{
    [SerializeField] private int _damage;
    [SerializeField] private float _attackRate;

    public int Damage => _damage;
    public float AttackRate => _attackRate;
}
