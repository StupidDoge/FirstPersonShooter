using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _count;
    
    public void SetUI(Sprite sprite, int count)
    {
        _image.sprite = sprite;
        _count.text = count.ToString();
    }
}
