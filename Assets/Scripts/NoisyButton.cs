using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void NoisyButtonEventHandler();

public class NoisyButton : InteractibleObject
{
    [SerializeField] private Transform buttonBody = default;
    [SerializeField] private AnimationCurve buttonBodyPressCurve = default;
    [SerializeField] private float buttonBodyPressDuration = default;
    [SerializeField] private Vector3 buttonBodyPressScale = default;
    private Coroutine currentCoroutine;
    private Vector3 minScale;
    public event NoisyButtonEventHandler onButtonClicked;

    private void Start()
    {
        minScale = Vector3.Scale(buttonBody.localScale, buttonBodyPressScale);
    }

    protected override void PerformAction()
    {
        base.PerformAction();
        onButtonClicked?.Invoke();

        if (currentCoroutine != null) return;
        currentCoroutine = StartCoroutine(pressAnim());
    }

    private IEnumerator pressAnim()
    {
        StartCoroutine(Tween.ScaleYoyo(buttonBody, minScale, buttonBodyPressDuration, buttonBodyPressCurve));
        yield return new WaitForSeconds(buttonBodyPressDuration);
        currentCoroutine = null;
    }
}
