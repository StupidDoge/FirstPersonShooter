using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ItemContextMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TextMeshProUGUI _itemName;
    [SerializeField] private Button _useButton;
    [SerializeField] private Button _equipButton;

    public bool PointerInMenu { get; private set; }

    public void SetButtons(string itemName, bool equippable, bool usable)
    {
        _itemName.text = itemName;
        if (equippable)
            _equipButton.gameObject.SetActive(true);

        if (usable)
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
        Debug.Log("drop");
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
