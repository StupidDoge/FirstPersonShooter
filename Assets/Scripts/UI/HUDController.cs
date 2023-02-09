using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDController : MonoBehaviour
{
    [SerializeField] private GameObject _ammoPanel;
    [SerializeField] private TextMeshProUGUI _ammo;

    private int _currentAmmo;
    private int _ammoLeft;

    private readonly string _devider = " / ";

    private void OnEnable()
    {
        PlayerAttackController.OnWeaponEquipped += ShowAmmoPanel;
        PlayerAttackController.OnWeaponRemoved += HideAmmoPanel;
        RangeWeaponPhysicalItem.OnAmmoDecreased += DecreaseAmmo;
    }

    private void OnDisable()
    {
        PlayerAttackController.OnWeaponEquipped -= ShowAmmoPanel;
        PlayerAttackController.OnWeaponRemoved -= HideAmmoPanel;
        RangeWeaponPhysicalItem.OnAmmoDecreased -= DecreaseAmmo;
    }

    private void ShowAmmoPanel(int currentAmmo, int totalAmmo)
    {
        _ammoPanel.SetActive(true);
        _currentAmmo = currentAmmo;
        _ammoLeft = Mathf.Clamp((totalAmmo - _currentAmmo), 0, totalAmmo);
        _ammo.text = _currentAmmo.ToString() + _devider + _ammoLeft.ToString();
    }

    private void DecreaseAmmo()
    {
        _currentAmmo--;
        _ammo.text = _currentAmmo.ToString() + _devider + _ammoLeft.ToString();
    }

    private void HideAmmoPanel()
    {
        _ammoPanel.SetActive(false);
    }
}
