using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private PlayerInputHolder _inputHolder;
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRadius = 0.5f;
    [SerializeField] private LayerMask _interactionLayer;

    private int _itemsFound;

    private readonly Collider[] _colliders = new Collider[3];

    private void OnEnable()
    {
        _inputHolder.OnInteractButtonPressed += Interact;
    }

    private void OnDisable()
    {
        _inputHolder.OnInteractButtonPressed -= Interact;
    }

    private void Interact()
    {
        _itemsFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, _interactionLayer);

        if (_itemsFound > 0)
        {
            var interactable = _colliders[0].GetComponent<IInteractable>();

            if (interactable != null)
                interactable.Interact(this);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
    }
}
