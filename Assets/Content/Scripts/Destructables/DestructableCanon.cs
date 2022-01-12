using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableCanon : Destructable
{
    private GameObject Player;

    public float detectionRadius = 10;

    public float shootInterval = 1.0f;

    public Bullet bulletPrefab;

    public Transform bulletSpawnPoint;

    public float upShootForce = 5;
    public float targetShootForce = 10;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        StartCoroutine(shootCanonBullet());
    }

    IEnumerator shootCanonBullet()
    {
        yield return new WaitForSeconds(shootInterval);

        if (isPlayerInRange())
        {
            Bullet b = Instantiate<Bullet>(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);

            b.GetComponent<Rigidbody>().AddForce(transform.up * upShootForce, ForceMode.Impulse);

            yield return new WaitForSeconds(shootInterval / 2.0f);

            b.GetComponent<Rigidbody>().AddForce((Player.transform.position - b.transform.position).normalized * targetShootForce, ForceMode.Impulse);
            b.GetComponent<Rigidbody>().useGravity = true;
        }

        StartCoroutine(shootCanonBullet());
    }

    private bool isPlayerInRange()
    {
        if (Vector3.Distance(transform.position, Player.transform.position) < detectionRadius)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override void OnDestructableDestroy()
    {
        base.OnDestructableDestroy();
        Destroy(gameObject);
    }
}
