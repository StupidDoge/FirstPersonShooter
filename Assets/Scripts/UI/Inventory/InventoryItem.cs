using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class InventoryItem : MonoBehaviour
{
    public static Action<InventoryItem, int> OnContextMenuOpened;

    public int CellNumber { get; set; }
    public bool IsEquipped { get; set; }
    public int WeaponCurrentAmmoAmount { get; private set; }
    [field: SerializeField] public ItemBase ItemSO { get; private set; }

    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _amountText;

    private int _amount;

    public void SetInfo(Sprite sprite, int amount, int cellNumber, PhysicalItemBase item, ItemBase itemSO)
    {
        _image.sprite = sprite;
        _amount = amount;
        _amountText.text = amount.ToString();
        CellNumber = cellNumber;
        if (item is RangeWeaponPhysicalItem tempItem)
        {
            WeaponCurrentAmmoAmount = tempItem.CurrentAmmo;
        }
        ItemSO = itemSO;
    }

    public void UpdateCount(int newCount)
    {
        _amountText.text = (_amount + newCount).ToString();
        _amount += newCount;
    }

    public void OpenContextMenu()
    {
        if (!IsEquipped)
            OnContextMenuOpened?.Invoke(this, _amount);
    }
}
