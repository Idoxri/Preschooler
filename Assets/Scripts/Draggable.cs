using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Draggable : MonoBehaviour
{
    public event System.Action OnBeginDrag;
    public event System.Action OnUpdateDrag;
    public event System.Action OnEndDrag;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb =  GetComponent<Rigidbody2D>();
    }

    public void BeginDrag()
    {
        //rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezePosition;

        OnBeginDrag.Invoke();
    }
    public void UpdateDrag()
    {
        OnUpdateDrag?.Invoke();
    }
    public void EndDrag(Vector2 velocity)
    {
        //rb.isKinematic = false;
        rb.constraints = RigidbodyConstraints2D.None;
        rb.velocity = velocity;

        OnEndDrag.Invoke();
    }
}
