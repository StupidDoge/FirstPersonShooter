using UnityEngine;
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
        RangeWeaponPhysicalItem.OnCurrentAmmoAmountChanged += ChangeAmmoAmount;
        RangeWeaponPhysicalItem.OnWeaponEquipped += ShowAmmoPanel;
        RangeWeaponPhysicalItem.OnWeaponUnequipped += HideAmmoPanel;
    }

    private void OnDisable()
    {
        RangeWeaponPhysicalItem.OnCurrentAmmoAmountChanged -= ChangeAmmoAmount;
        RangeWeaponPhysicalItem.OnWeaponEquipped -= ShowAmmoPanel;
        RangeWeaponPhysicalItem.OnWeaponUnequipped -= HideAmmoPanel;
    }

    private void ShowAmmoPanel(int currentAmmo, int totalAmmo)
    {
        _ammoPanel.SetActive(true);
        _currentAmmo = currentAmmo;
        _ammoLeft = Mathf.Clamp((totalAmmo - _currentAmmo), 0, totalAmmo);
        _ammo.text = _currentAmmo.ToString() + _devider + _ammoLeft.ToString();
    }

    private void ChangeAmmoAmount(int currentAmmo, int totalAmmo)
    {
        _currentAmmo = currentAmmo;
        _ammoLeft = totalAmmo - currentAmmo;

        if (_ammoLeft < 0)
        {
            int temp = _ammoLeft;
            _ammoLeft += _currentAmmo;
            _currentAmmo += temp;
        }

        _ammo.text = _currentAmmo.ToString() + _devider + _ammoLeft.ToString();
    }

    private void HideAmmoPanel()
    {
        _ammoPanel.SetActive(false);
    }
}
