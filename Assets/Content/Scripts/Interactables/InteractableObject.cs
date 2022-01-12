using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    protected IInteractableAction action;

    public bool conditionalAction = false;

    public virtual void OnTriggerEnterWithCondition(Collider other)
    { }

    public virtual void OnTriggerExitWithCondition(Collider other)
    { }

    private void Update()
    {
        if (action != null)
        {
            action.Update();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (conditionalAction)
        {
            OnTriggerEnterWithCondition(other);
        }
        else
        {
            if (action != null)
            {
                action.Start();
            }
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (conditionalAction)
        {
            OnTriggerExitWithCondition(other);
        }
        else
        {
            if (action != null)
            {
                action.Stop();
            }
        }
    }
}
