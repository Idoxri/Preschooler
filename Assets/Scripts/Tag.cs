using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tag : MonoBehaviour
{
    [SerializeField] private Draggable draggableComponent = default;
    [SerializeField] private BoxCollider2D coll = default;

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
        coll.isTrigger = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fruit"))
        {
            transform.SetParent(collision.transform);
            /*transform.GetComponent<Rigidbody2D>().gravityScale = 0f;
            transform.GetComponent<Rigidbody2D>().mass = 0f;
            transform.GetComponent<Rigidbody2D>().angularDrag = 0f;*/

            draggableComponent.enabled = false;
            coll.enabled = false;
            draggableComponent.OnEndDrag -= DraggableComponent_OnEndDrag;
        }
    }

    private void Update()
    {
        if (transform.parent != null)
            transform.position = transform.parent.position;
    }

    private void DraggableComponent_OnBeginDrag()
    {
        coll.isTrigger = true;
    }
}
