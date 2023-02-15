using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PhysicalWeaponItem : PhysicalItemBase
{
    public bool CanAttack { get; protected set; } = true;

    public override void Interact(Interactor interactor)
    {
        throw new System.NotImplementedException();
    }

    public virtual void Equip()
    {
        transform.localRotation = Quaternion.identity;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<BoxCollider>().enabled = false;
    }

    public virtual void Attack()
    {

    }

    public virtual void Unequip()
    {

    }
}
