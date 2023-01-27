using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;

    private bool _interacted = false;

    public string InteractionPrompt => _prompt;

    public bool Interact(Interactor interactor)
    {
        if (_interacted)
            return true;

        _interacted = true;
        Debug.Log("Opening chest");

        return true;
    }
}
