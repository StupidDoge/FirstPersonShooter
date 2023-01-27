using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PhysicalItemBase : MonoBehaviour, IInteractable
{
    public abstract void Interact(Interactor interactor);
}
