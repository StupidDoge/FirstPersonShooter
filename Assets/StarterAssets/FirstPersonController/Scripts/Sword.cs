using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;

    private bool _interacted = false;

    public string InteractionPrompt => _prompt;

    public bool Interact(Interactor interactor)
    {
        if (_interacted)
            return true;

        _interacted = true;
        Debug.Log("Pick up " + _prompt);

        return true;
    }
}
