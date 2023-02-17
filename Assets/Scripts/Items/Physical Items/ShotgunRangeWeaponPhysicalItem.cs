using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ShotgunRangeWeaponPhysicalItem : RangeWeaponPhysicalItem, IAimable
{
    public override void Attack()
    {
        base.Attack();
        Debug.Log("SHOTGUN ATTACKS");
    }

    public void Aim(bool aimInput)
    {
        if (aimInput)
            transform.localPosition = WeaponTemplate.AimPosition;
        else
            transform.localPosition = WeaponTemplate.HoldOffset;
    }

}
