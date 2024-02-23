using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Draggable : MonoBehaviour
{
    public event System.Action OnBeginDrag;
    public event System.Action OnUpdateDrag;
    public event System.Action OnEndDrag;

    [SerializeField] private float speed = 1;
    [SerializeField] private float forward = 1;

    [Header("Feedback")]
    [SerializeField] private float selectedScale = 1.1f;
    [SerializeField] private float selectedScaleDuration = 0.1f;
    [SerializeField] private AnimationCurve selectedScaleCurve = AnimationCurve.EaseInOut(0,0,1,1);
    [SerializeField] private ParticleSystem beginDragParticles = default;
    [SerializeField] private ParticleSystem endDragParticles = default;

    private Rigidbody2D rb;
    private Vector3 baseScale;
    private Vector3 velocity;

    private void Awake()
    {
        rb =  GetComponent<Rigidbody2D>();
        baseScale = transform.localScale;
    }

    public void BeginDrag()
    {
        StartCoroutine(Tween.Scale(transform, transform.localScale, baseScale * selectedScale, selectedScaleDuration, selectedScaleCurve));
        OnBeginDrag?.Invoke();

        if (beginDragParticles) Instantiate(beginDragParticles, transform.position, transform.rotation);
    }
    public void UpdateDrag(Vector3 worldTarget)
    {
        velocity = Vector3.MoveTowards(velocity, (worldTarget - transform.position) * forward, Time.deltaTime * speed);
        rb.velocity = velocity;
        OnUpdateDrag?.Invoke();
    }
    public void EndDrag(Vector2 velocity)
    {
        StartCoroutine(Tween.Scale(transform, transform.localScale, baseScale, selectedScaleDuration, selectedScaleCurve));
        OnEndDrag?.Invoke();

        if (endDragParticles) Instantiate(endDragParticles, transform.position, transform.rotation);
    }
}
