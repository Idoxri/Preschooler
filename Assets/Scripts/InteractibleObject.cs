using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleObject : MonoBehaviour
{
    private void Awake()
    {
        TouchController.Instance.OnDown += TouchController_OnDown;
    }

    private void TouchController_OnDown(Vector2 point)
    {
        PerformAction();
    }

    protected virtual void PerformAction()
    {
    }
}
