using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public enum CollectableType
{
    STAR,
    HEALTH,
    HAZARD,
    CLUE,
    TREASURE
}
public abstract class Collectable : MonoBehaviour
{
    public delegate void PickedUpEvent(CollectableType type, GameObject gameObject);

    public event PickedUpEvent onPickedUp, onAnimationCompleteEvent;
    internal CollectableType collectableType;
    public string[] allowedTagsToCollide;
	
	public abstract void OnPickedUp(CollectableType collectableType/*CollectableController collectableController*/);
    private void OnTriggerEnter(Collider other)
    {
        if (allowedTagsToCollide != null && allowedTagsToCollide.Length > 0)
        {
            if (allowedTagsToCollide.Contains(other.tag))
            {
                OnPickedUp(collectableType);
                onPickedUp?.Invoke(collectableType, gameObject);
                GetComponent<Collider>().enabled = false;
            }
        }
        else
        {
            OnPickedUp(collectableType);
            onPickedUp?.Invoke(collectableType, gameObject);
            GetComponent<Collider>().enabled = false;
        }
        
    }
}
