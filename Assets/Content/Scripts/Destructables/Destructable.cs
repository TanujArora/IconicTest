using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    public delegate void DestructableEvent(GameObject gameObject);

    public event DestructableEvent onDestructableHit;
    public event DestructableEvent onDestructableDestroy;

    public int destructableLife = 10;

    public virtual void OnDestructableHit() { }
    public virtual void OnDestructableDestroy() { }

   
    //Hit event to be managed by qualified hitting object
    public void DecreaseLife(int damage)
    {
        destructableLife -= damage;
        destructableLife = destructableLife < 0 ? 0 : destructableLife;

        onDestructableHit?.Invoke(gameObject);
        OnDestructableHit();

        if (destructableLife == 0)
        {
            onDestructableDestroy?.Invoke(gameObject);
            OnDestructableDestroy();
        }
    }

}
