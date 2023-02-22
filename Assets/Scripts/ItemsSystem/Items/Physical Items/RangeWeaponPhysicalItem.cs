using System;
using System.Collections;
using UnityEngine;

namespace ItemsSystem
{
    public class RangeWeaponPhysicalItem : PhysicalWeaponItem, IReloadable, IAimable
    {
        public static event Action<int, int> OnWeaponEquipped;
        public static event Action OnWeaponUnequipped;
        public static Action<AmmoType> OnWeaponShot;
        public static Action<int, int> OnCurrentAmmoAmountChanged;
        public static event Func<AmmoType, int> OnRangeWeaponEquipped;

        [SerializeField] protected ParticleSystem muzzleFlash;
        private int _ammoClip;
        private float _fireRate;
        private float _reloadTime;
        private Camera _mainCamera;

        public int AmmoClip => _ammoClip;
        public float FireRate => _fireRate;
        public Camera MainCamera => _mainCamera;

        public RangeWeaponSO RangeWeaponTemplate { get; private set; }
        public bool CanShoot { get; protected set; } = true;
        public bool IsReloading { get; protected set; } = false;
        public int TotalAmmo { get; set; }
        [field: SerializeField] public int CurrentAmmo { get; protected set; }

        private void Awake()
        {
            if (!(BaseTemplate is RangeWeaponSO))
                Debug.LogError(name + " item has been assigned with wrong template!");

            RangeWeaponTemplate = (RangeWeaponSO)BaseTemplate;
            SetWeaponStats();
        }

        protected override void Start()
        {
            base.Start();
            _mainCamera = Camera.main;
        }

        private void OnEnable()
        {
            Inventory.OnAmmoAmountChanged += UpdateTotalAmmoAmount;
        }

        private void OnDisable()
        {
            Inventory.OnAmmoAmountChanged -= UpdateTotalAmmoAmount;
        }

        private void SetWeaponStats()
        {
            _ammoClip = RangeWeaponTemplate.AmmoClip;
            _fireRate = RangeWeaponTemplate.FireRate;
            _reloadTime = RangeWeaponTemplate.ReloadTime;
        }

        public override void Interact()
        {
            OnPickupAudioClipTriggered?.Invoke(RangeWeaponTemplate.ItemPickupSound);
            OnItemEquipped?.Invoke(RangeWeaponTemplate, baseAmount, gameObject);
            OnItemEquipped?.Invoke(RangeWeaponTemplate.AmmoBoxPrefab.AmmoTemplate, CurrentAmmo, gameObject);
        }

        public void SetCurrentAmmo(int amount)
        {
            if (amount < 0)
            {
                Debug.LogError("Wrong ammo amount!");
                return;
            }

            CurrentAmmo = amount;
        }

        public override void Equip()
        {
            base.Equip();
            transform.localPosition = RangeWeaponTemplate.HoldOffset;
            transform.localRotation = RangeWeaponTemplate.HoldRotation;
            SwayIntensity = RangeWeaponTemplate.SwayIntensity;
            SwaySmooth = RangeWeaponTemplate.SwaySmooth;
            TotalAmmo = (int)OnRangeWeaponEquipped?.Invoke(RangeWeaponTemplate.WeaponAmmoType);
            if (TotalAmmo <= _ammoClip)
                CurrentAmmo = TotalAmmo;
            OnWeaponEquipped?.Invoke(CurrentAmmo, TotalAmmo);
            OnWeaponEquipSoundTriggered?.Invoke(RangeWeaponTemplate.EquipSound);
        }

        public override void Unequip()
        {
            base.Unequip();
            OnWeaponUnequipped?.Invoke();
        }

        public virtual void Aim(bool aimInput)
        {
            if (aimInput)
                transform.localPosition = RangeWeaponTemplate.AimPosition;
            else
                transform.localPosition = RangeWeaponTemplate.HoldOffset;
        }

        public virtual IEnumerator Reload()
        {
            IsReloading = true;

            int ammoToReload;
            if (TotalAmmo > AmmoClip)
                ammoToReload = AmmoClip;
            else
                ammoToReload = TotalAmmo;

            if (TotalAmmo == 0)
                CurrentAmmo = 0;
            else
                CurrentAmmo = ammoToReload;

            yield return new WaitForSeconds(_reloadTime);
            OnCurrentAmmoAmountChanged?.Invoke(CurrentAmmo, TotalAmmo);
            IsReloading = false;
        }

        protected IEnumerator AttackCoroutine()
        {
            CanAttack = false;
            yield return new WaitForSeconds(FireRate);
            CanAttack = true;
        }

        protected void PlayMuzzleFlash()
        {
            if (muzzleFlash.isPlaying)
                muzzleFlash.Stop();
            muzzleFlash.Play();
        }

        private void UpdateTotalAmmoAmount(AmmoType ammoType, int amount)
        {
            if (RangeWeaponTemplate.WeaponAmmoType == ammoType)
            {
                TotalAmmo += amount;
                if (InPlayerHands)
                    OnCurrentAmmoAmountChanged?.Invoke(CurrentAmmo, TotalAmmo);
            }
        }
    }
}
