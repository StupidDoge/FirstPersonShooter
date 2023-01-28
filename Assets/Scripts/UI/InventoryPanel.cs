using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private GameObject _contentContainer;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private GameObject _inventoryItem;

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
        foreach (KeyValuePair<ItemBase, int> item in _inventory.InventoryDictionary)
        {
            GameObject newObject = Instantiate(_inventoryItem, _contentContainer.transform);
            InventoryItem inventoryItem = newObject.GetComponent<InventoryItem>();
            inventoryItem.SetUI(item.Key.ItemSprite, item.Value);
        }
    }

    private void ClearInventory() // Must be improved
    {
        foreach (Transform cell in _contentContainer.transform)
        {
            Destroy(cell.gameObject);
        }
    }
}
