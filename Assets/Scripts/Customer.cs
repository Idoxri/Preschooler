using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    private CustomerSetting customerSetting;
    [SerializeField] private AnimationCurve slideCurve = default;
    [SerializeField] private float slideDuration = 1f;
    private Vector3 initialPosRelativeToCamera;

    public List<Item> ItemList { get; private set; } = new List<Item>();

    private void Start()
    {
        CameraController.Instance.onMoveDone += CameraController_onMoveDone;
        initialPosRelativeToCamera = transform.position - Camera.main.transform.position ;
    }

    private void CameraController_onMoveDone()
    {
        StartCoroutine(MoveInView());
    }

    private IEnumerator MoveInView()
    {
        StartCoroutine(Tween.Move(transform.position, Camera.main.transform.position + initialPosRelativeToCamera, transform, slideDuration, slideCurve));
        yield return null;
    }

    public void SetInformations(CustomerSetting setting)
    {
        customerSetting = setting;

        for (int i = 0; i < customerSetting.ItemList.Count; i++)
        {
            ItemList.Add(customerSetting.ItemList[i]);
        }
    }
}
