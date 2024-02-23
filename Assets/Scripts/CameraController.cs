using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void CameraControllerEventHandler();

[DefaultExecutionOrder(1000)]
public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; private set; }

    [SerializeField] private Vector3 pointA = default;
    [SerializeField] private Vector3 pointB = default;
    [SerializeField] private float slideDuration = default;
    [SerializeField] private AnimationCurve slideCurve = default;
    private bool isAtPointA = true;
    private Coroutine currentCoroutine;

    public event CameraControllerEventHandler onMoveDone;

    private void Awake()
    {
        Instance = this;
        TouchController.Instance.OnDown += Instance_OnDown;
    }

    private void Start()
    {
        SlideToOtherPoint();
    }

    private void Instance_OnDown(Vector2 point)
    {
        if (currentCoroutine != null) return;
        currentCoroutine = StartCoroutine(SlideToOtherPoint());
    }

    public IEnumerator SlideToOtherPoint()
    {
        if (isAtPointA)
        {
            StartCoroutine(Tween.Move(transform.position, pointB, transform, slideDuration, slideCurve));
            isAtPointA = false;
        }
        else
        {
            StartCoroutine(Tween.Move(transform.position, pointA, transform, slideDuration, slideCurve));
            isAtPointA = true;
        }

        yield return new WaitForSeconds(slideDuration);
        currentCoroutine = null;
        onMoveDone?.Invoke();
        yield return null;
        
    }
}
