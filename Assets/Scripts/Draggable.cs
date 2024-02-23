using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Draggable : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Awake()
    {
        rb =  GetComponent<Rigidbody2D>();
    }

    public void BeginDrag()
    {
        //rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
    }
    public void UpdateDrag()
    {

    }
    public void EndDrag(Vector2 velocity)
    {
        //rb.isKinematic = false;
        rb.constraints = RigidbodyConstraints2D.None;
        rb.velocity = velocity;
    }
}
