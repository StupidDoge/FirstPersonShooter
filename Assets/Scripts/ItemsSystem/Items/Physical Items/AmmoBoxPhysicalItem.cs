using UnityEngine;

namespace ItemsSystem
{
    public class AmmoBoxPhysicalItem : PhysicalItemBase
    {
        [SerializeField] private AmmoSO _ammoSO;
        [SerializeField] private int _amount;

        private float _damage;
        private AmmoType _ammoType;

        public AmmoSO AmmoTemplate => _ammoSO;

        protected override void Start()
        {
            base.Start();
            SetAmmoBoxStats();
        }

        private void SetAmmoBoxStats()
        {
            _damage = _ammoSO.Damage;
            _ammoType = _ammoSO.Type;
        }

        public void SetAmount(int amount)
        {
            _amount = amount;
        }

        public override void Interact()
        {
            OnPickupAudioClipTriggered?.Invoke(_ammoSO.ItemPickupSound);
            OnItemEquipped?.Invoke(_ammoSO, _amount, gameObject);
        }
    }
}
