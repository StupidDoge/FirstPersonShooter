using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class ItemContextMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static event Action<ItemBase, int> OnItemDropped;
    public static Action<InventoryItem> OnItemEquipped;

    [SerializeField] private TextMeshProUGUI _itemName;
    [SerializeField] private Button _useButton;
    [SerializeField] private Button _equipButton;

    public bool PointerInMenu { get; private set; }

    private ItemBase _itemSO;
    private InventoryItem _item;
    private Inventory _inventory;
    private int _amount;

    private void Start()
    {
        _inventory = FindObjectOfType<Inventory>();
    }

    public void SetContextMenu(InventoryItem inventoryItem, int amount)
    {
        _item = inventoryItem;
        _itemSO = inventoryItem.ItemSO;
        _amount = amount;
        _itemName.text = inventoryItem.ItemSO.Name;
        if (inventoryItem.ItemSO.Equippable)
            _equipButton.gameObject.SetActive(true);

        if (inventoryItem.ItemSO.Usable)
            _useButton.gameObject.SetActive(true);
    }

    public void UseItem()
    {
        Debug.Log("use");
    }

    public void EquipItem()
    {
        if (_inventory.ActiveItem != null)
            return;

        _item.IsEquipped = true;
        OnItemEquipped?.Invoke(_item);
        gameObject.SetActive(false);
    }

    public void DropItem()
    {
        OnItemDropped?.Invoke(_itemSO, _amount);
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        _useButton.gameObject.SetActive(false);
        _equipButton.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        PointerInMenu = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PointerInMenu = false;
    }
}
