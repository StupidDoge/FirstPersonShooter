using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponPhysicalItem : PhysicalWeaponItem
{
    [SerializeField] private MeleeWeaponSO _meleeWeaponSO;

    public MeleeWeaponSO MeleeWeaponTemplate => _meleeWeaponSO;

    public override void Interact(Interactor interactor)
    {
        OnItemEquipped?.Invoke(_meleeWeaponSO, baseAmount, gameObject);
    }

    public override void Equip()
    {
        base.Equip();
        Debug.Log("MELEE EQUIPPED");
        transform.localPosition = _meleeWeaponSO.HoldOffset;
    }

    public override void Attack()
    {
        base.Attack();
        Debug.Log("MELEE ATTACKS");
    }
}
