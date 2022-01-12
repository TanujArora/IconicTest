using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletVelocity = 1;
    public int bulletDamage = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Destructable>())
        {
            other.GetComponent<Destructable>().DecreaseLife(1);
            Destroy(gameObject);
        }
        else if (other.GetComponent<CharacterController>())
        {
            other.GetComponent<CharacterController>().TakeDamage(bulletDamage);
        }
        else 
            Destroy(gameObject);
    }
}
