using System.Collections.Generic;
using UnityEngine;
using Core;

namespace ItemsSystem
{
    public class InventoryPanel : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private GameObject _contentContainer;
        [SerializeField] private Inventory _inventory;
        [SerializeField] private PlayerInputHolder _inputHolder;
        [SerializeField] private GameObject _inventoryItem;
        [SerializeField] private ItemContextMenu _itemContextMenu;
        [SerializeField] private Transform _activeItemCell;

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
            _inputHolder.OnInventoryUsed += InventoryPanelSetActive;
            _inputHolder.OnMouseLeftButtonClicked += CheckLeftMouseButtonClick;
            _inventory.OnItemAdded += AddItem;
            _inventory.OnItemUpdated += UpdateItem;
            _inventory.OnItemRemoved += RemoveItem;
            _inventory.OnActiveItemSet += MoveItemToActiveCell;
            _inventory.OnItemFromActiveSlotAdded += SearchFirstEmptyCell;
            InventoryItem.OnContextMenuOpened += OpenContextMenu;
            DraggableItem.OnItemDragged += SearchFirstEmptyCell;
        }

        private void OnDisable()
        {
            _inputHolder.OnInventoryUsed -= InventoryPanelSetActive;
            _inputHolder.OnMouseLeftButtonClicked -= CheckLeftMouseButtonClick;
            _inventory.OnItemAdded -= AddItem;
            _inventory.OnItemUpdated -= UpdateItem;
            _inventory.OnItemRemoved -= RemoveItem;
            _inventory.OnActiveItemSet -= MoveItemToActiveCell;
            _inventory.OnItemFromActiveSlotAdded -= SearchFirstEmptyCell;
            InventoryItem.OnContextMenuOpened -= OpenContextMenu;
            DraggableItem.OnItemDragged -= SearchFirstEmptyCell;
        }

        private void InventoryPanelSetActive()
        {
            if (_panel.activeInHierarchy)
            {
                _panel.SetActive(false);
                CloseContextMenu();
                // JUST FOR TESTING! DO IT PROPERLY LATER!
                FindObjectOfType<FirstPersonController>().GetComponent<FirstPersonController>().enabled = true;
                Cursor.lockState = CursorLockMode.Locked;
                GlobalUIController.AnyUIPanelIsActive = false;
            }
            else
            {
                _panel.SetActive(true);
                // JUST FOR TESTING! DO IT PROPERLY LATER!
                FindObjectOfType<FirstPersonController>().GetComponent<FirstPersonController>().enabled = false;
                Cursor.lockState = CursorLockMode.None;
                GlobalUIController.AnyUIPanelIsActive = true;
            }
        }

        private void AddItem(ItemBase item, int amount, GameObject physicalItem)
        {
            SearchFirstEmptyCell();
            GameObject newObject = Instantiate(_inventoryItem, _emptyCells[_firstEmptyCell].transform);
            InventoryItem inventoryItem = newObject.GetComponent<InventoryItem>();
            inventoryItem.SetInfo(item.ItemSprite, amount, _firstEmptyCell,
                physicalItem.GetComponent<PhysicalItemBase>(), item);
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

        private void RemoveItem(ItemBase item)
        {
            foreach (Transform cell in _contentContainer.transform)
            {
                if (cell.transform.childCount > 0)
                {
                    InventoryItem itemInCell = cell.GetComponentInChildren<InventoryItem>();
                    if (itemInCell.ItemSO == item)
                    {
                        Destroy(itemInCell.gameObject);
                    }
                }
            }
        }

        private void OpenContextMenu(InventoryItem inventoryItem, int amount)
        {
            _itemContextMenu.gameObject.SetActive(true);
            _itemContextMenu.SetContextMenu(inventoryItem, amount);
        }

        private void CloseContextMenu()
        {
            _itemContextMenu.gameObject.SetActive(false);
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

        private void CheckLeftMouseButtonClick()
        {
            if (!_itemContextMenu.PointerInMenu)
                CloseContextMenu();
        }

        private void MoveItemToActiveCell(ItemBase itemSO, InventoryItem item)
        {
            if (_activeItemCell.childCount != 0)
                return;

            foreach (Transform cell in _contentContainer.transform)
            {
                if (cell.transform.childCount != 0)
                {
                    if (cell.GetComponentInChildren<InventoryItem>().ItemSO == itemSO)
                    {
                        InventoryItem newItem = cell.GetComponentInChildren<InventoryItem>();
                        newItem.transform.SetParent(_activeItemCell);
                        newItem.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
                    }
                }
            }

            SearchFirstEmptyCell();
        }
    }
}
