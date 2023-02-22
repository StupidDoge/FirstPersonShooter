using System.Collections;
using UnityEngine;

namespace ItemsSystem
{
    public class MeleeWeaponPhysicalItem : PhysicalWeaponItem
    {
        private float _attackRate;

        public float AttackRate => _attackRate;

        public MeleeWeaponSO MeleeWeaponTemplate { get; private set; }

        private void Awake()
        {
            if (!(BaseTemplate is MeleeWeaponSO))
                Debug.LogError(name + " item has been assigned with wrong template!");

            MeleeWeaponTemplate = (MeleeWeaponSO)BaseTemplate;
        }

        protected override void Start()
        {
            base.Start();
            SetWeaponStats();
        }

        private void SetWeaponStats()
        {
            _attackRate = MeleeWeaponTemplate.AttackRate;
        }

        public override void Interact()
        {
            OnPickupAudioClipTriggered?.Invoke(MeleeWeaponTemplate.ItemPickupSound);
            OnItemEquipped?.Invoke(MeleeWeaponTemplate, baseAmount, gameObject);
        }

        public override void Equip()
        {
            base.Equip();
            transform.localPosition = MeleeWeaponTemplate.HoldOffset;
            transform.localRotation = MeleeWeaponTemplate.HoldRotation;
            SwayIntensity = MeleeWeaponTemplate.SwayIntensity;
            SwaySmooth = MeleeWeaponTemplate.SwaySmooth;
        }

        public override void Attack()
        {
            if (!CanAttack)
                return;

            Debug.Log("MELEE ATTACKS");
            StartCoroutine(AttackCoroutine());
        }

        private IEnumerator AttackCoroutine()
        {
            CanAttack = false;
            yield return new WaitForSeconds(_attackRate);
            CanAttack = true;
        }
    }
}
