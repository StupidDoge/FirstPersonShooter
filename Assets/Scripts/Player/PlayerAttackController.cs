using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    [SerializeField] private Transform _itemContainer;

    public bool ItemIsEquipped { get; private set; }

    private PhysicalWeaponItem _equippedWeapon;

    private PlayerInputHolder _playerInputHolder;

    private void Start()
    {
        _playerInputHolder = GetComponent<PlayerInputHolder>();
    }

    private void OnEnable()
    {
        Inventory.OnActiveItemSet += EquipItem;
        Inventory.OnActiveItemRemoved += UnequipItem;
        PlayerInputHolder.OnReloadButtonPressed += StartReloading;
        PlayerInputHolder.OnMouseRightButtonHold += Aim;
    }

    private void OnDisable()
    {
        Inventory.OnActiveItemSet -= EquipItem;
        Inventory.OnActiveItemRemoved -= UnequipItem;
        PlayerInputHolder.OnReloadButtonPressed -= StartReloading;
        PlayerInputHolder.OnMouseRightButtonHold -= Aim;
    }

    private void Aim(bool aimInput)
    {
        if (_equippedWeapon.TryGetComponent(out IAimable aimable))
            aimable.Aim(aimInput);
    }

    private void StartReloading()
    {
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
            weapon.CurrentAmmo = inventoryItem.WeaponCurrentAmmoAmount;
        _equippedWeapon.Equip();
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
}
