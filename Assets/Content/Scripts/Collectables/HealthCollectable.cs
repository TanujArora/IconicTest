using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectable : Collectable
{
    private void Awake()
    {
        collectableType = CollectableType.HEALTH;
    }
    public override void OnPickedUp(CollectableType collectableType)
	{
		
	}
}
