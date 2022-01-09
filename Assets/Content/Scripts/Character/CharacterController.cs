using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterController : MonoBehaviour
{
	private CharacterMovement characterMovement;
	private NavMeshAgent navAgent;
	private int maxHealth = 50;
	private int health;
    private bool isEquipped = false;
    public Transform handTransform;

    void Start()
    {
		characterMovement = GetComponent<CharacterMovement>();
		navAgent = GetComponent<NavMeshAgent>();
		//health = maxHealth;
	}

    void Update()
    {
		Vector3 movement = Vector3.zero;
        if(Input.GetKey(KeyCode.W))
		{
			movement.z = 1f;
		}
		else if(Input.GetKey(KeyCode.S))
		{
			movement.z = -1f;
		}
		if(Input.GetKey(KeyCode.A))
		{
			movement.x = -1f;
		}
		else if(Input.GetKey(KeyCode.D))
		{
			movement.x = 1f;
		}

		characterMovement.Move(movement);
	}

	public void SetHealth(int newHealth)
	{
		health = newHealth;
		health = Mathf.Min(health, maxHealth);
		health = Mathf.Max(health, 0);
	}

	public void BoostHealth()
	{
		health += 10;
		health = Mathf.Min(health, maxHealth);
	}

	public void TakeDamage()
	{
		health -= 10;
		health = Mathf.Max(health, 0);
	}

   

	public int MaxHealth
	{
		get { return maxHealth; }
	}

	public int Health
	{
		get { return health; }
	}

    public bool Equipped
    {
        get { return isEquipped; }
    }

}
