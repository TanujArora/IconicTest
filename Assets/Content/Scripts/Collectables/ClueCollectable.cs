using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueCollectable : Collectable
{
    public int ClueValue
    {
        get
        {
            return clueValue;
        }
    }

    [SerializeField] private int clueValue;

    private int clueIndex;

    private void Awake()
    {
        collectableType = CollectableType.CLUE;
    }

    public override void OnPickedUp(CollectableType collectableType)
    {
        

    }

    public string getClueValueString()
    {
        return $"Key {clueIndex} is {clueValue}";
    }

    public void SetClueValue(int index, int value)
    {
        this.clueIndex = index;
        this.clueValue = value;
    }
}
