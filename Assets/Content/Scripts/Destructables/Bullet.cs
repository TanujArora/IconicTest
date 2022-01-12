using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletVelocity = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Destructable>())
        {
            other.GetComponent<Destructable>().DecreaseLife(1);
        }
        else if (other.GetComponent<CharacterController>())
        {
            other.GetComponent<CharacterController>().TakeDamage();
        }
        Destroy(gameObject);
    }
}
