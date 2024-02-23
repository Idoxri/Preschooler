using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tag : MonoBehaviour
{
    [SerializeField] private Draggable draggableComponent = default;
    [SerializeField] private float interactionRadius = .5f;
    [SerializeField] private Collider coll = default;

    private void Start()
    {
        draggableComponent.OnBeginDrag += DraggableComponent_OnBeginDrag;
        draggableComponent.OnEndDrag += DraggableComponent_OnEndDrag;
    }

    private void OnDestroy()
    {
        draggableComponent.OnBeginDrag -= DraggableComponent_OnBeginDrag;
        draggableComponent.OnEndDrag -= DraggableComponent_OnEndDrag;
    }

    private void DraggableComponent_OnEndDrag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.collider.CompareTag("Fruit"))
            {
                transform.SetParent(hit.transform);
            }
        }
    }

    private void DraggableComponent_OnBeginDrag()
    {
        //coll.
    }
}
