using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ClassicRangeWeaponPhysicalItem : RangeWeaponPhysicalItem
{
    public override void Shoot()
    {
        base.Shoot();
        Debug.Log("Classic weapon shot");
    }

    public override Task Reload()
    {
        return base.Reload();
    }
}
