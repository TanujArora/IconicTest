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
    HAZARD
}
public abstract class Collectable : MonoBehaviour
{
    public delegate void PickedUpEvent(CollectableType type, GameObject gameObject);
    /*private CollectableController collectableController;*/

    public event PickedUpEvent onPickedUp, onAnimationCompleteEvent;
    internal CollectableType collectableType;
	/*public void Setup(CollectableController collectableController)
	{
		this.collectableController = collectableController;
	}*/
	public abstract void OnPickedUp(CollectableType collectableType/*CollectableController collectableController*/);
    private void OnTriggerEnter(Collider other)
    {
        OnPickedUp(collectableType);
        onPickedUp?.Invoke(collectableType, gameObject);
        GetComponent<Collider>().enabled = false;
    }
}
