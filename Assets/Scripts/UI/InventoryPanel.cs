using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
    [SerializeField] private GameObject _panel;

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
}
