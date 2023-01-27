using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRadius = 0.5f;
    [SerializeField] private LayerMask _interactionLayer;
    [SerializeField] private int _itemsFound;
    [SerializeField] private float _interactionInputTimeout;

    private PlayerInputHolder _inputHolder;

    private readonly Collider[] _colliders = new Collider[3];

    private void Start()
    {
        _inputHolder = GetComponent<PlayerInputHolder>();
    }

    private void Update()
    {
        _itemsFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, _interactionLayer);

        if (_itemsFound > 0)
        {
            var interactable = _colliders[0].GetComponent<IInteractable>();

            if (interactable != null && _inputHolder.interact)
            {
                interactable.Interact(this);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
    }
}
