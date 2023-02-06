using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class ItemContextMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static Action<ItemBase, int> OnItemDropped;

    [SerializeField] private TextMeshProUGUI _itemName;
    [SerializeField] private Button _useButton;
    [SerializeField] private Button _equipButton;

    public bool PointerInMenu { get; private set; }

    private ItemBase _itemSO;
    private int _amount;

    public void SetContextMenu(ItemBase itemSO, int amount)
    {
        _itemSO = itemSO;
        _amount = amount;
        _itemName.text = itemSO.Name + " " + _amount;
        if (itemSO.Equippable)
            _equipButton.gameObject.SetActive(true);

        if (itemSO.Usable)
            _useButton.gameObject.SetActive(true);
    }

    public void UseItem()
    {
        Debug.Log("use");
    }

    public void EquipItem()
    {
        Debug.Log("equip");
    }

    public void DropItem()
    {
        OnItemDropped?.Invoke(_itemSO, _amount);
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
