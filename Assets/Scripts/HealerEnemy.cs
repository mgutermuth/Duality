using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HealerEnemy : MonoBehaviour {

	private GameObject[] enemiesInGame;
	public float healTimer = 2.0f;
	public float moveSpeed = 7; //move speed
	public int rotationSpeed = 7; //speed of turning
	public int chaseThreshold = 10; // distance within which to start chasing
	public int giveUpThreshold = 20; // distance beyond which AI gives up
	public float coolDown = 2.5f;
	private float oscillate = .01f;
	private bool blarg = false;
	private bool chasing = false;
	public float health = 30.0f;
	public float bulletDamage = 10.0f;
	public float burnTime = 3.0f;
	public float slowTime = 3.0f;
	private float timeSlowed = 0f;
	private float timeBurning = 0f;
	private float delta = 0f;
	public float chargeShotDamage = 30.0f;
	public float jumpShotDamage = 15.0f;
    public float explosionDamage = 45.0f;
	public float fireDamagePerSecond = 5.0f;
	private float fireCoolDown = 1.0f;
	private bool isBurning = false;
	public float divideSpeed = 2;
	private float originalSpeed;
	private float distance;
	private bool isLiving = true;
	private GameObject target;
    private CharacterController theEnemy;
    private Vector3 targetPos;
	private bool slowed = false;
    public float hitBackDistance = 10.0f; 
	public float healingRadius = 10.0f;
	public float chaseRadius = 15.0f;
	
	
	void Start()
	{
        theEnemy = GetComponent<CharacterController>();	
		GetComponentInChildren<HealerRangeScript>().setRadius(healingRadius);
	}
	
	public void Update()
	{
		delta = Time.deltaTime;
		
        if (!isLiving)
        {
            DestroyMe();
        }
		
		if(isBurning && fireCoolDown <= 0)
		{
			health -= fireDamagePerSecond;
			if (health <= 0)
			{
				isLiving = false;
			}
			fireCoolDown = 1;
		}
		
		enemiesInGame = GameObject.FindGameObjectsWithTag("Enemy");
			
        transform.position = new Vector3(transform.position.x, 3.0f, transform.position.z);
		Vector3 temp = transform.position; // this whole blarg oscillate business is only intended as a temporary fix, I will have to look for a better one soon.
		if(!blarg)
		{
			temp.x += oscillate;
			transform.position = temp;
		}
		else
		{
			temp.x -= oscillate;
			transform.position = temp;
		}
		
		if(healTimer > 0)
		{
			healTimer -= delta;
		}
		if(fireCoolDown > 0)
		{
			fireCoolDown -= delta;
		}
		
		if (enemiesInGame!= null)
		{
			float positionTemp = (enemiesInGame[0].transform.position -transform.position).magnitude;
			foreach ( GameObject enemy in enemiesInGame)
			{
				Debug.Log((enemy.transform.position - transform.position).magnitude);
				if((enemy.transform.position - transform.position).magnitude < positionTemp)
				{
					positionTemp = (enemy.transform.position - transform.position).magnitude;
					target = enemy;
				}
			}
		}
			
		if (target != null)
		{
			targetPos = (target.transform.position - transform.position).normalized;      	
       

    		// check distance to target every frame:
    		 distance = (target.transform.position - transform.position).magnitude;

     		if (chasing)
    		 {

		         //rotate to look at the player
		         transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
		         //move towards the player
		      
		             theEnemy.Move( targetPos * Time.deltaTime*moveSpeed);
		         
		         // give up, if too far away from target:
		         if (distance > giveUpThreshold)
		         {
		             chasing = false;
		         }
		
		         // attack, if close enough, and if time is OK:

		     }
		     else
		     {
		         // not currently chasing.
		
		         // start chasing if target comes close enough
		         if (distance < chaseThreshold)
		         {
		             chasing = true;
		         }
		     }
		}
		blarg = !blarg; // see the blarg comment above
	}


void OnTriggerEnter(Collider myColl)
{
    if (myColl.name == "Bullet(Clone)")
    {	
		StartCoroutine("Flash");
        health -= bulletDamage;
        if (health <= 0)
        {
            isLiving = false;
        }

        myColl.GetComponent<BulletScript>().RemoveMe();
        
    }
	if (myColl.name	 == "ChargeShot(Clone)")
	{
		health-= chargeShotDamage;
			
		if (health <= 0)
        {
          	isLiving = false;
        }
	}

    if (myColl.name == "Robit(Clone)")
    {
        myColl.GetComponent<RobitController>().Explode();
    }
    if (myColl.name == "Explosion(Clone)")
    {
        health -= explosionDamage;
        if (health <= 0)
        {
            isLiving = false;
        }

    }
		
	if(myColl.name =="OilSpill(Clone)" && slowed == false)
	{
		originalSpeed = moveSpeed;		
      	moveSpeed /= divideSpeed;		
		slowed =true;	
	}

    if (myColl.name == "JumpShot(Clone)")
    {
        GetComponent<CharacterController>().Move(targetPos * -hitBackDistance);
	    health -= jumpShotDamage;
        if (health <= 0)
        {
            isLiving = false;
        }

    }
	
}
	
	void OnTriggerExit(Collider myColl)
	{
		if(myColl.name == "OilSpill(Clone)")
		{
			if (isBurning == true)
			{
				isBurning = false;
			}
			if(slowed == true)
			{
				moveSpeed = originalSpeed;
				slowed = false;
			}
		}
	}
	void OnTriggerStay(Collider myColl)
	{
		if(myColl.name =="OilSpill(Clone)")
		{
			if(myColl.GetComponent<OilScript>().isOnFire)
			{
				
				isBurning = true;
			}
		}	
	}
		
	IEnumerator Flash ()
	{
		renderer.material.color = Color.red;
		yield return new WaitForSeconds (.1f);
		renderer.material.color = Color.white;
	}
	
	

	void DestroyMe()
	{
		Destroy(gameObject);
	}
	void Heal()
	{	
	    if( healTimer <= 0)
		{
           
		}
		
	}
	
}
	


