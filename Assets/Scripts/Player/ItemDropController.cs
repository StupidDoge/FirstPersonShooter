using UnityEngine;

public class ItemDropController : MonoBehaviour
{
    [SerializeField] private Transform _dropPoint;

    private void OnEnable()
    {
        ItemContextMenu.OnItemDropped += SpawnItem;
    }

    private void OnDisable()
    {
        ItemContextMenu.OnItemDropped -= SpawnItem;
    }

    private void SpawnItem(ItemBase itemSO, int amount)
    {
        GameObject newItem = Instantiate(itemSO.ItemPrefab);
        newItem.transform.position = _dropPoint.position;

        if (newItem.TryGetComponent(out AmmoBoxPhysicalItem ammoBox))
        {
            ammoBox.SetAmount(amount);
        }

        if (newItem.TryGetComponent(out RangeWeaponPhysicalItem rangeWeapon))
        {
            rangeWeapon.SetCurrentAmmo(0);
        }
    }
}
