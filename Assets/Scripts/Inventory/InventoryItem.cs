using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public int CellNumber { get; set; }
    public ItemBase ItemSO { get; private set; }

    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _amountText;

    private int _amount;
    
    public void SetInfo(Sprite sprite, int amount, int cellNumber, ItemBase itemSO)
    {
        _image.sprite = sprite;
        _amount = amount;
        _amountText.text = amount.ToString();
        CellNumber = cellNumber;
        ItemSO = itemSO;
    }

    public void UpdateCount(int newCount)
    {
        _amountText.text = (_amount + newCount).ToString();
    }
}
