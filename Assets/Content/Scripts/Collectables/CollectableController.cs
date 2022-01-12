using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class CollectableController
{
    private Collectable[] collectables;

	public CollectableController(Transform collectableContainer, CharacterController character)
	{
		collectables = collectableContainer.GetComponentsInChildren<Collectable>();
		foreach(Collectable collectable in collectables)
		{
            collectable.onPickedUp += Collectable_onPickedUp;
		}
        
	}

    //This will return only class of type collectables
    public T[] getCollectableList<T>(CollectableType t)
    {
        T[] filteredList = collectables.Where(x => x.collectableType == t && x.GetComponent<Collectable>()).Select(x => x.GetComponent<T>()).ToArray();
        return filteredList;
    }

    private void Collectable_onPickedUp(CollectableType type, GameObject _object)
    {
        switch (type)
        {
            case CollectableType.STAR:

                StarController starController = GameObject.FindObjectOfType<StarController>();
                starController.PickupStar();
                _object.GetComponent<Animator>().SetTrigger("pickedUp");

                _object.GetComponent<StarCollectable>().DelayCall(_object.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length * 2, () =>
                {
                    OnPickedUp(_object.GetComponent<Collectable>());
                });
                break;
            case CollectableType.HEALTH:

                CharacterController character = GameObject.FindObjectOfType<CharacterController>();
                character.BoostHealth();
                OnPickedUp(_object.GetComponent<Collectable>());

                break;
            case CollectableType.HAZARD:

                CharacterController _character = GameObject.FindObjectOfType<CharacterController>();
                _character.TakeDamage();
                OnPickedUp(_object.GetComponent<Collectable>());

                break;
            case CollectableType.CLUE:

                OnPickedUp(_object.GetComponent<Collectable>());

                //Use UI to display the clue!!

                break;
        }
    }

    public void OnPickedUp(Collectable collectable)
	{
        collectable.onPickedUp -= Collectable_onPickedUp;
		GameObject.Destroy(collectable.gameObject);
	}

    
}
