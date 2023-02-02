using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private GameObject _contentContainer;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private GameObject _inventoryItem;

    private List<InventoryCell> _emptyCells;

    private void Start()
    {
        _emptyCells = new List<InventoryCell>();
        foreach (Transform cell in _contentContainer.transform)
            _emptyCells.Add(cell.GetComponent<InventoryCell>());

        Debug.Log(_emptyCells.Count);
    }

    private void OnEnable()
    {
        PlayerInputHolder.OnInventoryUsed += InventoryPanelSetActive;
    }

    private void OnDisable()
    {
        PlayerInputHolder.OnInventoryUsed -= InventoryPanelSetActive;
    }

    private void InventoryPanelSetActive()
    {
        if (_panel.activeInHierarchy)
        {
            _panel.SetActive(false);
            ClearInventory();
            // JUST FOR TESTING! DO IT PROPERLY LATER!
            FindObjectOfType<FirstPersonController>().GetComponent<FirstPersonController>().enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            _panel.SetActive(true);
            ShowInventory();
            // JUST FOR TESTING! DO IT PROPERLY LATER!
            FindObjectOfType<FirstPersonController>().GetComponent<FirstPersonController>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void ShowInventory() // Must be improved
    {
        int i = 0;
        foreach (KeyValuePair<ItemBase, int> item in _inventory.InventoryDictionary)
        {
            GameObject newObject = Instantiate(_inventoryItem, _emptyCells[i].transform);
            InventoryItem inventoryItem = newObject.GetComponent<InventoryItem>();
            inventoryItem.SetUI(item.Key.ItemSprite, item.Value);
            _emptyCells[i].Item = inventoryItem;
            i++;
        }

        foreach (KeyValuePair<ItemBase, int> cell in _inventory.InventoryDictionary)
            Debug.Log(cell.Key.GetType() + ", amount = " + cell.Value);
    }

    private void ClearInventory() // Must be improved
    {
        foreach (InventoryCell cell in _emptyCells)
            cell.DeleteItem();
    }
}
