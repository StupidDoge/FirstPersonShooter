using System.Collections;
using UnityEngine;

namespace ItemsSystem
{
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
            PlayMuzzleFlash();
            AudioSource.PlayOneShot(RangeWeaponTemplate.ShotSound);
            OnWeaponShot?.Invoke(RangeWeaponTemplate.WeaponAmmoType);
            OnCurrentAmmoAmountChanged?.Invoke(CurrentAmmo, TotalAmmo);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log("CLASSIC hits " + hit.collider.gameObject.name);
            }

            StartCoroutine(AttackCoroutine());
        }

        public override void Aim(bool aimInput)
        {
            base.Aim(aimInput);
        }

        public override IEnumerator Reload()
        {
            Debug.Log("CLASSIC RELOAD");
            return base.Reload();
        }
    }
}
