using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBoxPhysicalItem : PhysicalItemBase
{
    [SerializeField] private AmmoSO _ammoSO;
    [SerializeField] private float _amount;

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

    public override void Interact(Interactor interactor)
    {
        Debug.Log(_ammoSO.Name + " equipped! Damage = " + _damage + ", ammo type = " + _ammoType);
        Destroy(gameObject);
    }
}
