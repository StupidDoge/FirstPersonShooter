using UnityEngine;

public class ClassicRangeWeaponPhysicalItem : RangeWeaponPhysicalItem
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
            Debug.Log("CLASSIC hits " + hit.collider.gameObject.name);
        }

        StartCoroutine(AttackCoroutine());
    }
}
