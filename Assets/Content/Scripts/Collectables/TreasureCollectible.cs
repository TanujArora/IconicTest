using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureCollectible : Collectable
{
    private void Awake()
    {
        collectableType = CollectableType.TREASURE;
    }
    public override void OnPickedUp(CollectableType collectableType)
    {
    }
}
