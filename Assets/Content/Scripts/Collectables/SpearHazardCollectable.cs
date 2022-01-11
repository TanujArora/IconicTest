using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearHazardCollectable : Collectable
{
    private void Awake()
    {
        collectableType = CollectableType.HAZARD;
    }
    public override void OnPickedUp(/*CollectableController collectableController*/CollectableType collectableType)
	{
		/*CharacterController character = FindObjectOfType<CharacterController>();
		character.TakeDamage();
		collectableController.OnPickedUp(this);*/
	}
}
