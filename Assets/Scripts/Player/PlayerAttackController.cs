using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    [field: SerializeField] public int PistolAmmoAmount { get; private set; }
    [field: SerializeField] public int RifleAmmoAmount { get; private set; }
    [field: SerializeField] public int ShotgunAmmoAmount { get; private set; }

    private void OnEnable()
    {
        Inventory.OnAmmoAmountChanged += ChangeAmmoAmount;
    }

    private void OnDisable()
    {
        Inventory.OnAmmoAmountChanged -= ChangeAmmoAmount;
    }

    private void ChangeAmmoAmount(AmmoType ammoType, int amount)
    {
        switch (ammoType)
        {
            case AmmoType.Pistol:
                PistolAmmoAmount = amount;
                break;
            case AmmoType.Rifle:
                RifleAmmoAmount = amount;
                break;
            case AmmoType.Shotgun:
                ShotgunAmmoAmount = amount;
                break;
        }
    }
}
