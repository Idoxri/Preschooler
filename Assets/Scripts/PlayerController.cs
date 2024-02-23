using UnityEngine;

[DefaultExecutionOrder(100)]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float forceMultiplier = 1;
    private Draggable currentDraggable;
    private Vector2 lastPosition;

    private void OnEnable()
    {
        TouchController.Instance.OnDown += TouchController_OnDown;
        TouchController.Instance.OnHold += TouchController_OnHold;
        TouchController.Instance.OnUp += TouchController_OnUp;
    }
    private void OnDisable()
    {
        if(!TouchController.Instance)
                return;
        TouchController.Instance.OnDown -= TouchController_OnDown;
        TouchController.Instance.OnHold -= TouchController_OnHold;
        TouchController.Instance.OnUp -= TouchController_OnUp;
    }

    private void TouchController_OnDown(Vector2 point)
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(point);
        RaycastHit2D hit =  Physics2D.Raycast(worldPoint, worldPoint);

        if(hit.rigidbody != null && hit.rigidbody.TryGetComponent(out currentDraggable))
        {
            currentDraggable.BeginDrag();
        }
    }
    private void TouchController_OnHold(Vector2 point)
    {
        if (!currentDraggable)
            return;

        lastPosition = Camera.main.ScreenToWorldPoint(point);
        currentDraggable.transform.position = lastPosition;
        currentDraggable.UpdateDrag();
    }
    private void TouchController_OnUp(Vector2 point)
    {
        if (currentDraggable)
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(point);

            currentDraggable.EndDrag((worldPoint - lastPosition) * forceMultiplier);
            currentDraggable = null;
        }
    }
}
