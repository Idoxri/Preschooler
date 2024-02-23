using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tween : MonoBehaviour
{
    public static IEnumerator Scale(Transform target, Vector3 to, float duration, AnimationCurve curve = null)
    {
        if (curve == null)
            curve = AnimationCurve.Linear(0, 0, 1, 1);

        float elapsedTime = 0f;
        Vector3 startSize = target.localScale;

        while (elapsedTime <= duration)
        {
            target.localScale = Vector3.LerpUnclamped(startSize, to, curve.Evaluate(elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        target.localScale = to;
        yield break;
    }

    public static IEnumerator Scale(Transform target, Vector3 from, Vector3 to, float duration, AnimationCurve curve = null)
    {
        if (curve == null)
            curve = AnimationCurve.Linear(0, 0, 1, 1);

        float elapsedTime = 0f;

        while (elapsedTime <= duration)
        {
            target.localScale = Vector3.LerpUnclamped(from, to, curve.Evaluate(elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        target.localScale = to;
        yield break;
    }

    public static IEnumerator ScaleYoyo(Transform target, Vector3 to, float duration, AnimationCurve curve = null)
    {
        float elapsedTime = 0f;
        Vector3 startSize = target.localScale;

        while (elapsedTime <= (duration / 2))
        {
            target.localScale = Vector3.LerpUnclamped(startSize, to, curve.Evaluate(elapsedTime / (duration / 2)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0f;
        target.localScale = to;

        while (elapsedTime <= (duration / 2))
        {
            target.localScale = Vector3.LerpUnclamped(to, startSize, curve.Evaluate(elapsedTime / (duration / 2)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        target.localScale = startSize;
    }

    public static IEnumerator Delay(float duration)
    {
        yield return new WaitForSeconds(duration);
    }

    public static IEnumerator ColorUI(Image image, Color from, Color to, float duration, AnimationCurve curve = null)
    {
        curve ??= AnimationCurve.Linear(0, 0, 1, 1);

        float elapsedTime = 0f;
        while (elapsedTime <= duration)
        {
            image.color = Color.LerpUnclamped(from, to, curve.Evaluate(elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        image.color = to;
    }


    public static IEnumerator FadeUI(Image image, float duration)
    {
        float elapsedTime = 0f;
        Color startColor = image.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        while (elapsedTime <= duration)
        {
            image.color = Color.LerpUnclamped(startColor, endColor, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    public static IEnumerator FadeUI(SpriteRenderer sprite, float duration)
    {
        float elapsedTime = 0f;
        Color startColor = sprite.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        while (elapsedTime <= duration)
        {
            sprite.color = Color.LerpUnclamped(startColor, endColor, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    public static IEnumerator FadeUI(SpriteRenderer sprite, float duration, float maxAlpha)
    {
        float elapsedTime = 0f;
        Color startColor = sprite.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, maxAlpha);

        while (elapsedTime <= duration)
        {
            sprite.color = Color.LerpUnclamped(startColor, endColor, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
    public static IEnumerator FadeUI(Graphic graphic, float duration, float from, float to)
    {
        float elapsedTime = 0f;

        Color startColor = graphic.color;
        startColor.a = from;

        Color endColor = startColor;
        endColor.a = to;

        while (elapsedTime <= duration)
        {
            graphic.color = Color.LerpUnclamped(startColor, endColor, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        graphic.color = endColor;
    }

    public static IEnumerator Move(Vector3 startPos, Vector3 endPos, Transform objectToMove, float duration, AnimationCurve curve = null)
    {
        if (curve == null)
            curve = AnimationCurve.Linear(0, 0, 1, 1);

        float elapsedTime = 0f;
        objectToMove.position = startPos;

        while (elapsedTime <= duration)
        {
            elapsedTime += Time.deltaTime;
            objectToMove.position = Vector3.LerpUnclamped(startPos, endPos, curve.Evaluate(elapsedTime / duration));
            yield return null;
        }

        objectToMove.position = endPos;
    }

    public static IEnumerator OscillateAnchored(RectTransform target, Vector2 startPos, Vector2 offset, float duration, float frequency)
    {
        float elapsedTime = 0f;

        while (elapsedTime <= duration)
        {
            elapsedTime += Time.deltaTime;

            target.anchoredPosition = startPos + (offset * Mathf.Sin(elapsedTime * frequency)); ;

            yield return null;
        }

        target.anchoredPosition = startPos;
    }

    public static IEnumerator Rotate(Transform objectToRotate, Vector3 axis, float angle, float duration, AnimationCurve curve = null)
    {
        if (curve == null)
            curve = AnimationCurve.Linear(0, 0, 1, 1);

        float elapsedTime = 0f;
        Quaternion startRotation = objectToRotate.rotation;
        Quaternion targetQuaternion = Quaternion.AngleAxis(angle, axis) * startRotation;

        while (elapsedTime <= duration)
        {
            elapsedTime += Time.deltaTime;
            objectToRotate.rotation = Quaternion.SlerpUnclamped(startRotation, targetQuaternion, curve.Evaluate(elapsedTime / duration));
            yield return null;
        }

        objectToRotate.rotation = targetQuaternion;
    }

    public static IEnumerator Rotate(Transform objectToRotate, Vector3 axis, float from, float to, float duration, AnimationCurve curve = null)
    {
        if (curve == null)
            curve = AnimationCurve.Linear(0, 0, 1, 1);

        float elapsedTime = 0f;
        Quaternion startRotation = Quaternion.AngleAxis(from, axis) * objectToRotate.rotation;
        Quaternion targetQuaternion = Quaternion.AngleAxis(to, axis) * objectToRotate.rotation;

        while (elapsedTime <= duration)
        {
            elapsedTime += Time.deltaTime;
            objectToRotate.rotation = Quaternion.SlerpUnclamped(startRotation, targetQuaternion, curve.Evaluate(elapsedTime / duration));
            yield return null;
        }

        objectToRotate.rotation = targetQuaternion;
    }
}

