using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleObject : MonoBehaviour
{
    [SerializeField] private float interactionRadius = .5f;

    private void Awake()
    {
        TouchController.Instance.OnDown += TouchController_OnDown;
    }

    private void TouchController_OnDown(Vector2 point)
    {
        Camera mainCamera = Camera.main;
        float camOffset = mainCamera.transform.position.z;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(point.x, point.y, -camOffset));

        if(Vector3.Distance(mousePos, transform.position) <= interactionRadius)
        {
            PerformAction();
        }

    }


    protected virtual void PerformAction()
    {
    }
}
