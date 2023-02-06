using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBoxPhysicalItem : PhysicalItemBase
{
    [SerializeField] private AmmoSO _ammoSO;
    [SerializeField] private int _amount;

    private float _damage;
    private AmmoType _ammoType;

    private void Start()
    {
        SetAmmoBoxStats();
    }

    private void SetAmmoBoxStats()
    {
        _damage = _ammoSO.Damage;
        _ammoType = _ammoSO.Type;
    }

    public void SetAmount(int amount)
    {
        _amount = amount;
    }

    public override void Interact(Interactor interactor)
    {
        OnItemEquipped?.Invoke(_ammoSO, _amount);
        Destroy(gameObject);
    }
}
