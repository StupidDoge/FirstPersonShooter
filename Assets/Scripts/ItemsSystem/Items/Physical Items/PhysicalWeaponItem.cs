using UnityEngine;

namespace ItemsSystem
{
    public abstract class PhysicalWeaponItem : PhysicalItemBase
    {
        public bool CanAttack { get; protected set; } = true;
        public float SwayIntensity { get; protected set; }
        public float SwaySmooth { get; protected set; }
        public bool InPlayerHands { get; protected set; }

        protected override void Start()
        {
            base.Start();
        }

        public override void Interact()
        {
            throw new System.NotImplementedException();
        }

        public virtual void Equip()
        {
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<BoxCollider>().enabled = false;
            InPlayerHands = true;
        }

        public virtual void Attack()
        {

        }

        public virtual void Unequip()
        {
            InPlayerHands = false;
        }
    }
}
