using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    [SerializeField] private Transform _itemContainer;
    [field: SerializeField] public int PistolAmmoAmount { get; private set; }
    [field: SerializeField] public int RifleAmmoAmount { get; private set; }
    [field: SerializeField] public int ShotgunAmmoAmount { get; private set; }

    public bool ItemIsEquipped { get; private set; }

    private void OnEnable()
    {
        Inventory.OnAmmoAmountChanged += ChangeAmmoAmount;
        Inventory.OnActiveItemSet += EquipItem;
        Inventory.OnActiveItemRemoved += UnequipItem;
    }

    private void OnDisable()
    {
        Inventory.OnAmmoAmountChanged -= ChangeAmmoAmount;
        Inventory.OnActiveItemSet -= EquipItem;
        Inventory.OnActiveItemRemoved -= UnequipItem;
    }

    private void ChangeAmmoAmount(AmmoType ammoType, int amount)
    {
        switch (ammoType)
        {
            case AmmoType.Pistol:
                PistolAmmoAmount = amount;
                break;
            case AmmoType.Rifle:
                RifleAmmoAmount = amount;
                break;
            case AmmoType.Shotgun:
                ShotgunAmmoAmount = amount;
                break;
        }
    }

    private void EquipItem(ItemBase itemSO)
    {
        if (ItemIsEquipped)
            return;

        itemSO.ItemPrefab.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        GameObject item = Instantiate(itemSO.ItemPrefab, _itemContainer);
        item.GetComponent<Rigidbody>().isKinematic = true;
        item.GetComponent<BoxCollider>().enabled = false;
        ItemIsEquipped = true;
    }

    private void UnequipItem()
    {
        Destroy(_itemContainer.GetComponentInChildren<PhysicalItemBase>().gameObject);
        ItemIsEquipped = false;
    }
}
