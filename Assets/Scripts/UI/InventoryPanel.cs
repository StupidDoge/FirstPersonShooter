using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private GameObject _contentContainer;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private GameObject _inventoryItem;

    private List<InventoryCell> _emptyCells;

    [SerializeField] private int _firstEmptyCell;

    private void Start()
    {
        _emptyCells = new List<InventoryCell>();
        int i = 0;
        foreach (Transform cell in _contentContainer.transform)
        {
            InventoryCell inventoryCell = cell.GetComponent<InventoryCell>();
            _emptyCells.Add(inventoryCell);
            inventoryCell.Id = i;
            i++;
        }
    }

    private void OnEnable()
    {
        PlayerInputHolder.OnInventoryUsed += InventoryPanelSetActive;
        Inventory.OnItemAdded += AddItem;
        Inventory.OnItemUpdated += UpdateItem;
    }

    private void OnDisable()
    {
        PlayerInputHolder.OnInventoryUsed -= InventoryPanelSetActive;
        Inventory.OnItemAdded -= AddItem;
        Inventory.OnItemUpdated -= UpdateItem;
    }

    private void InventoryPanelSetActive()
    {
        if (_panel.activeInHierarchy)
        {
            _panel.SetActive(false);
            // JUST FOR TESTING! DO IT PROPERLY LATER!
            FindObjectOfType<FirstPersonController>().GetComponent<FirstPersonController>().enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            _panel.SetActive(true);
            // JUST FOR TESTING! DO IT PROPERLY LATER!
            FindObjectOfType<FirstPersonController>().GetComponent<FirstPersonController>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void AddItem(ItemBase item, int amount)
    {
        SearchFirstEmptyCell();
        GameObject newObject = Instantiate(_inventoryItem, _emptyCells[_firstEmptyCell].transform);
        InventoryItem inventoryItem = newObject.GetComponent<InventoryItem>();
        inventoryItem.SetInfo(item.ItemSprite, amount, _firstEmptyCell, item);
        _emptyCells[_firstEmptyCell].Item = inventoryItem;
        _firstEmptyCell++;
    }

    private void UpdateItem(ItemBase item, int amount)
    {
        foreach (Transform cell in _contentContainer.transform)
        {
            if (cell.transform.childCount > 0)
            {
                InventoryItem itemInCell = cell.GetComponentInChildren<InventoryItem>();
                if (itemInCell.ItemSO == item)
                {
                    itemInCell.UpdateCount(amount);
                }
            }
        }
    }

    private void SearchFirstEmptyCell()
    {
        foreach (Transform cell in _contentContainer.transform)
        {
            if (cell.transform.childCount == 0)
            {
                _firstEmptyCell = cell.GetComponent<InventoryCell>().Id;
                break;
            }
        }
    }
}
