using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
    [SerializeField] private GameObject _panel;

    private void OnEnable()
    {
        StarterAssets.PlayerInputHolder.OnInventoryUsed += InventoryPanelSetActive;
    }

    private void OnDisable()
    {
        StarterAssets.PlayerInputHolder.OnInventoryUsed -= InventoryPanelSetActive;
    }

    private void InventoryPanelSetActive()
    {
        if (_panel.activeInHierarchy)
        {
            _panel.SetActive(false);
            // JUST FOR TESTING! DO IT PROPERLY LATER!
            /*FindObjectOfType<StarterAssets.FirstPersonController>().GetComponent<StarterAssets.FirstPersonController>().enabled = true;
            Cursor.lockState = CursorLockMode.Locked;*/
        }
        else
        {
            _panel.SetActive(true);
            // JUST FOR TESTING! DO IT PROPERLY LATER!
            FindObjectOfType<StarterAssets.FirstPersonController>().GetComponent<StarterAssets.FirstPersonController>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
