using System;
using UnityEngine;

public abstract class PhysicalItemBase : MonoBehaviour, IInteractable
{
    public static Action<ItemBase, int, GameObject> OnItemEquipped;

    public readonly int baseAmount = 1;

    public abstract void Interact(Interactor interactor);
}
