using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperRangeWeaponPhysicalItem : RangeWeaponPhysicalItem, IAimable
{
    public override void Attack()
    {
        if (CurrentAmmo == 0 && TotalAmmo != 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (!CanAttack || IsReloading || TotalAmmo == 0)
            return;

        Ray ray = MainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        ray.origin = transform.position;
        CurrentAmmo--;
        OnWeaponShot?.Invoke(WeaponTemplate.WeaponAmmoType);
        OnCurrentAmmoAmountChanged?.Invoke(CurrentAmmo, TotalAmmo);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log("Sniper rifle hits " + hit.collider.gameObject.name);
        }

        StartCoroutine(AttackCoroutine());
    }

    public void Aim(bool aimInput)
    {
        Debug.Log("SCOPE");
    }
}
