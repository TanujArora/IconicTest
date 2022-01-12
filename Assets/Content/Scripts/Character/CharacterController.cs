using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterController : MonoBehaviour
{
	private CharacterMovement characterMovement;
	private NavMeshAgent navAgent;
	private Animator animator;
	private int maxHealth = 50;
	private int health;
    private bool isEquipped = false;
	private Vector3 targetLookAtPoint;
    public Transform handTransform;
	public bool shouldLookAtMousePosition = false;
	public bool canShoot = false;
	public Transform shootPointTransform;
	public Bullet bulletPrefab;
	public bool disableControls = false;

    void Start()
    {
		characterMovement = GetComponent<CharacterMovement>();
		navAgent = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator>();
		//health = maxHealth;
	}

    void Update()
    {
		if (disableControls)
		{
			characterMovement.Move(Vector3.zero);
			return;
		}

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

		if (shouldLookAtMousePosition)
		{
			UpdateLookAtPoint();
		}

		if (canShoot)
		{
			if (Input.GetMouseButtonDown(0))
			{
				ShootBullet();
			}
		}
	}

	void UpdateLookAtPoint()
	{
		Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit = new RaycastHit();

		if (Physics.Raycast(r, out hit, Mathf.Infinity))
		{
			if (hit.transform.tag == "Ground")
			{
				targetLookAtPoint =  new Vector3(hit.point.x, shootPointTransform.position.y, hit.point.z);
			}
		}
	}

	void ShootBullet()
	{
		

		float m_Angle = Vector3.Angle(shootPointTransform.forward, (targetLookAtPoint - shootPointTransform.position).normalized);


		if (m_Angle > 90)
			return;


		Bullet b = Instantiate<Bullet>(bulletPrefab, shootPointTransform.position, Quaternion.identity);
		Vector3 targetDirection = (targetLookAtPoint - shootPointTransform.position).normalized;//transform.forward;

		b.GetComponent<Rigidbody>().AddForce(targetDirection * b.bulletVelocity, ForceMode.Impulse);
		Destroy(b.gameObject, 10);
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

    private void OnAnimatorIK(int layerIndex)
    {
		if (shouldLookAtMousePosition)
		{
			animator.SetLookAtWeight(0.5f);
			animator.SetLookAtPosition(targetLookAtPoint);
		}
		else
		{
			animator.SetLookAtWeight(0);
		}
		
		
    }

}
