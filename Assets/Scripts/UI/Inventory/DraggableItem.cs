using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public static event Action OnItemDragged;

    private CanvasGroup _canvasGroup;
    public Transform ParentAfterDrag { get; set; }

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Input.GetMouseButton(0))
        {
            _canvasGroup.alpha = 0.75f;
            _canvasGroup.blocksRaycasts = false;
            ParentAfterDrag = transform.parent;
            transform.SetParent(transform.root);
            transform.SetAsLastSibling();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Input.GetMouseButton(0))
            transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Input.GetMouseButtonUp(0))
        {
            _canvasGroup.alpha = 1f;
            _canvasGroup.blocksRaycasts = true;
            transform.SetParent(ParentAfterDrag);
            OnItemDragged?.Invoke();
        }
    }
}
