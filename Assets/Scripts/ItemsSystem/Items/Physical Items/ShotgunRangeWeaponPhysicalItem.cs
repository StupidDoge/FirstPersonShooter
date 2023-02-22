using System.Collections;
using UnityEngine;

namespace ItemsSystem
{
    public class ShotgunRangeWeaponPhysicalItem : RangeWeaponPhysicalItem
    {
        private ShotgunWeaponSO _shotgunWeaponSO;

        public override void Equip()
        {
            base.Equip();
            _shotgunWeaponSO = (ShotgunWeaponSO)WeaponTemplate;
        }

        public override void Attack()
        {
            if (CurrentAmmo == 0 && TotalAmmo != 0)
            {
                StartCoroutine(Reload());
                return;
            }

            if (!CanAttack || IsReloading || TotalAmmo == 0)
                return;

            for (int i = 0; i < _shotgunWeaponSO.AmountOfRays; i++)
            {
                Vector3 direction = MainCamera.transform.forward;
                Vector3 spread = Vector3.zero;
                spread += MainCamera.transform.up * Random.Range(-1f, 1f);
                spread += MainCamera.transform.right * Random.Range(-1f, 1f);
                direction += spread.normalized * Random.Range(0f, 0.2f);

                Ray ray = MainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f));
                ray.origin = transform.position + direction;

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    Debug.DrawLine(MainCamera.transform.position, hit.point, Color.green, 1f);
                    Debug.Log("SHOTGUN HIT " + hit.collider.gameObject.name);
                }
                else
                {
                    Debug.DrawLine(MainCamera.transform.position, MainCamera.transform.position + direction * 25f, Color.red, 1f);
                }
            }
            CurrentAmmo--;
            PlayMuzzleFlash();
            audioSource.PlayOneShot(_shotgunWeaponSO.ShotSound);
            OnWeaponShot?.Invoke(WeaponTemplate.WeaponAmmoType);
            OnCurrentAmmoAmountChanged?.Invoke(CurrentAmmo, TotalAmmo);

            StartCoroutine(AttackCoroutine());
        }

        public override void Aim(bool aimInput)
        {
            base.Aim(aimInput);
        }

        public override IEnumerator Reload()
        {
            Debug.Log("SHOTGUN RELOAD");
            return base.Reload();
        }
    }
}
