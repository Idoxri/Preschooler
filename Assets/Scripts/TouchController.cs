using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public delegate void TouchEventHandler(Vector2 point);

public class TouchController : MonoBehaviour
{
    public static TouchController Instance { get; private set; }
    public bool Down { get; private set; }
    public Vector2 Position { get; private set; }
    public Vector2 LastPosition { get; private set; }
    public Vector2 Delta => Position - LastPosition;
    public event TouchEventHandler OnUp, OnDown, OnHold;

    private void Awake()
    {
        Instance = this;
    }
    private void OnDestroy()
    {
        Instance = null;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Down = true;
            RefreshPosition();
            OnDown?.Invoke(Position);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Down = false;
            RefreshPosition();
            OnUp?.Invoke(Position);
        }
        else if (Input.GetMouseButton(0))
        {
            LastPosition = Position;
            Position = Input.mousePosition;
            OnHold?.Invoke(Position);
        }
    }

    private void RefreshPosition()
    {
        Position = Input.mousePosition;
        LastPosition = Position;
    }

    public static bool IsOverUI()
    {
        EventSystem eventSystem = EventSystem.current;
        if (eventSystem)
        {
#if UNITY_EDITOR
            return eventSystem.IsPointerOverGameObject();
#else
                //Specific to mobile devices, need to check every finger
                for (int i = 0; i < Input.touchCount; i++)
                {
                    if (eventSystem.IsPointerOverGameObject(i))
                        return true;
                }
#endif
        }

        return false;
    }
}
