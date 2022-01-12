using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateInteractable : InteractableObject
{
    public Transform moveTransform;
    public Vector3 targetPosition;

    public delegate void GateEvent(IInteractableAction action);

    public event GateEvent OnCollidedWithGate;

    private void Awake()
    {
        action = new GateOpenAction(moveTransform, moveTransform.localPosition, targetPosition);
    }

    public override void OnTriggerEnterWithCondition(Collider other)
    {
        base.OnTriggerEnterWithCondition(other);
        if (other.GetComponent<CharacterController>())
        {
            OnCollidedWithGate?.Invoke(action);
        }
    }

    public override void OnTriggerExitWithCondition(Collider other)
    {
        base.OnTriggerExitWithCondition(other);
    }
}
