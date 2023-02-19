using UnityEngine;
using Core;

namespace ItemsSystem
{
    public class PlayerItemHolder : MonoBehaviour
    {
        [SerializeField] private Transform _itemContainer;

        public bool ItemIsEquipped { get; private set; }

        private PhysicalWeaponItem _equippedWeapon;
        private PlayerInputHolder _playerInputHolder;
        private Inventory _inventory;

        #region Sway fields

        private Quaternion _originalWeaponRotation, _xAdjustment, _yAdjustment, _targetRotation;

        private float _inputX;
        private float _inputY;
        private float _swayIntensity;
        private float _swaySmooth;

        #endregion

        private void Awake()
        {
            _playerInputHolder = GetComponent<PlayerInputHolder>();
            _inventory = GetComponent<Inventory>();
        }

        private void OnEnable()
        {
            _inventory.OnActiveItemSet += EquipItem;
            _inventory.OnActiveItemRemoved += UnequipItem;
            _playerInputHolder.OnReloadButtonPressed += StartReloading;
            _playerInputHolder.OnMouseRightButtonHold += Aim;
        }

        private void OnDisable()
        {
            _inventory.OnActiveItemSet -= EquipItem;
            _inventory.OnActiveItemRemoved -= UnequipItem;
            _playerInputHolder.OnReloadButtonPressed -= StartReloading;
            _playerInputHolder.OnMouseRightButtonHold -= Aim;
        }

        private void Aim(bool aimInput)
        {
            if (_equippedWeapon != null)
                if (_equippedWeapon.TryGetComponent(out IAimable aimable))
                    aimable.Aim(aimInput);
        }

        private void StartReloading()
        {
            if (_equippedWeapon != null)
                if (_equippedWeapon.TryGetComponent(out IReloadable reloadable))
                    StartCoroutine(reloadable.Reload());
        }

        private void Update()
        {
            if (!GlobalUIController.AnyUIPanelIsActive && _equippedWeapon != null)
            {
                if (_playerInputHolder.leftMouseClick)
                {
                    _equippedWeapon.Attack();
                }

                UpdateWeaponSway();
            }
        }

        private void EquipItem(ItemBase itemSO, InventoryItem inventoryItem)
        {
            if (ItemIsEquipped)
                return;

            GameObject item = Instantiate(itemSO.ItemPrefab, _itemContainer);
            var newWeapon = item.GetComponent<PhysicalWeaponItem>();
            _equippedWeapon = newWeapon;
            if (_equippedWeapon.TryGetComponent(out RangeWeaponPhysicalItem weapon))
                weapon.SetCurrentAmmo(inventoryItem.WeaponCurrentAmmoAmount);
            _equippedWeapon.Equip();
            _originalWeaponRotation = _equippedWeapon.transform.localRotation;
            _swayIntensity = _equippedWeapon.SwayIntensity;
            _swaySmooth = _equippedWeapon.SwaySmooth;
        }

        private int UnequipItem()
        {
            int ammo = -1;

            if (_equippedWeapon.TryGetComponent(out RangeWeaponPhysicalItem weapon))
                ammo = weapon.CurrentAmmo;

            _equippedWeapon.Unequip();
            Destroy(_itemContainer.GetComponentInChildren<PhysicalItemBase>().gameObject);
            ItemIsEquipped = false;
            _equippedWeapon = null;

            return ammo;
        }

        private void UpdateWeaponSway()
        {
            _inputX = _playerInputHolder.look.x;
            _inputY = _playerInputHolder.look.y;

            _xAdjustment = Quaternion.AngleAxis(-_swayIntensity * _inputX, Vector3.up);
            _yAdjustment = Quaternion.AngleAxis(_swayIntensity * _inputY, Vector3.right);
            _targetRotation = _originalWeaponRotation * _xAdjustment * _yAdjustment;

            _equippedWeapon.transform.localRotation = Quaternion.Lerp(_equippedWeapon.transform.localRotation, _targetRotation, Time.deltaTime * _swaySmooth);
        }
    }
}
