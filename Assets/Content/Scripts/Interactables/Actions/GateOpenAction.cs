using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateOpenAction : IInteractableAction
{
    private Transform transform;
    private Vector3 startValue;
    private Vector3 endValue;

    private IEnumerator moveRoutine;
    public GateOpenAction(Transform transform, Vector3 startValue, Vector3 endValue)
    {
        this.transform = transform;
        this.startValue = startValue;
        this.endValue = endValue;
    }

    public void Start()
    {
        moveRoutine = MoveRoutine(endValue);
    }

    public void Stop()
    {
        moveRoutine = MoveRoutine(endValue);
    }

    public void Update()
    {
        if (moveRoutine != null)
        {
            if (!moveRoutine.MoveNext())
            {
                moveRoutine = null;
            }
        }
    }

    private IEnumerator MoveRoutine(Vector3 targetValue)
    {
        Vector3 originalPosition = transform.localPosition;
        Vector3 targetPosition = targetValue;

        float lerpPercentage = 0.0f;
        while (lerpPercentage < 1.0f)
        {
            transform.localPosition = Vector3.Lerp(originalPosition, targetPosition, lerpPercentage); ;
            lerpPercentage += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = targetPosition;
    }
}
