using System;
using UnityEngine;

namespace ItemsSystem
{
    public abstract class PhysicalWeaponItem : PhysicalItemBase
    {
        public static Action<AudioClip> OnWeaponEquipSoundTriggered;

        [SerializeField] private WeaponSO _weaponSO;
        [SerializeField] private int _interactableLayer;
        [SerializeField] private int _weaponLayer;

        public WeaponSO BaseTemplate => _weaponSO;
        public bool CanAttack { get; protected set; } = true;
        public float SwayIntensity { get; protected set; }
        public float SwaySmooth { get; protected set; }
        public bool InPlayerHands { get; protected set; }

        protected override void Start()
        {
            base.Start();
        }

        public virtual void Equip()
        {
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<BoxCollider>().enabled = false;
            InPlayerHands = true;
            SetItemLayer(_weaponLayer);
        }

        public virtual void Attack() { }

        public virtual void Unequip()
        {
            InPlayerHands = false;
            SetItemLayer(_interactableLayer);
        }

        private void SetItemLayer(int newLayer)
        {
            transform.gameObject.layer = newLayer;
            foreach (Transform weaponPart in transform)
                weaponPart.gameObject.layer = newLayer;
        }
    }
}