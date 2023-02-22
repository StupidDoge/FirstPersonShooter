using System.Collections;
using UnityEngine;

namespace ItemsSystem
{
    public class MeleeWeaponPhysicalItem : PhysicalWeaponItem
    {
        [SerializeField] private MeleeWeaponSO _meleeWeaponSO;

        private float _attackRate;

        public MeleeWeaponSO MeleeWeaponTemplate => _meleeWeaponSO;

        public float AttackRate => _attackRate;

        protected override void Start()
        {
            base.Start();
            SetWeaponStats();
        }

        private void SetWeaponStats()
        {
            _attackRate = _meleeWeaponSO.AttackRate;
        }

        public override void Interact()
        {
            OnPickupAudioClipTriggered?.Invoke(_meleeWeaponSO.ItemPickupSound);
            OnItemEquipped?.Invoke(_meleeWeaponSO, baseAmount, gameObject);
        }

        public override void Equip()
        {
            base.Equip();
            transform.localPosition = _meleeWeaponSO.HoldOffset;
            transform.localRotation = _meleeWeaponSO.HoldRotation;
            SwayIntensity = _meleeWeaponSO.SwayIntensity;
            SwaySmooth = _meleeWeaponSO.SwaySmooth;
            OnWeaponEquipSoundTriggered?.Invoke(_meleeWeaponSO.EquipSound);
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
