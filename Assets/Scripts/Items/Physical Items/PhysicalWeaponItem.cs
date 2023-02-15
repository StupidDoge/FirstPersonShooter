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
