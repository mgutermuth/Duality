using UnityEngine;
using System.Collections;

public class BaseEnemy : MonoBehaviour {


    private GameObject playerOne;
	private GameObject playerTwo;
	public float moveSpeed = 7; //move speed
	public int rotationSpeed = 7; //speed of turning
	public int attackThreshold = 7; // distance within which to attack
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
	private float attackTimer = 0;
    private CharacterController theEnemy;
    private Vector3 targetPos;
	private bool slowed = false;
	private bool deathPlayed;
    public float hitBackDistance = 10.0f;
    private GameObject soundManager;
	void Start()
		
	{
        theEnemy = GetComponent<CharacterController>();
        playerOne = GameObject.Find("PlayerOne");
		playerTwo = GameObject.Find("PlayerTwo");
		target = playerOne;
        soundManager = GameObject.Find("SoundManager");
			
	}
public void Update()
	{
		if(isLiving && !animation["Death"].enabled && !animation["Run"].enabled && !animation["Attack"].enabled)
		{
			animation.Play("Idle");
		}
		delta = Time.deltaTime;
		
        if (!isLiving)
        {
			
			if(deathPlayed &&!animation["Death"].enabled)
			{
            	DestroyMe();
			}
			if(!animation["Death"].enabled)
			{
				
				deathPlayed = true;
			}
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
			
        transform.position = new Vector3(transform.position.x, 0.0f, transform.position.z);
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
		
		if(attackTimer > 0)
		{
			attackTimer -= delta;
		}
		if(fireCoolDown > 0)
		{
			fireCoolDown -= delta;
		}
		
		
    if (playerTwo != null)
        {
            float tempDistance = (playerOne.transform.position - transform.position).magnitude;
			float tempDistance2 = (playerTwo.transform.position - transform.position).magnitude;
            if (tempDistance < tempDistance2)
            {
                target = playerOne;
            }
            else
            {
                target = playerTwo;
            }
        }
		if (target != null)
		{
			targetPos = (target.transform.position - transform.position).normalized;      	
       

    		// check distance to target every frame:
    		 distance = (target.transform.position - transform.position).magnitude;

     		if (chasing && isLiving)
    		 {

		         //rotate to look at the player
		         transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
		         //move towards the player
		         if (distance > attackThreshold)
		         {
					 animation.Play("Run");
		             theEnemy.Move( targetPos * Time.deltaTime*moveSpeed);
		         }
		         // give up, if too far away from target:
		         if (distance > giveUpThreshold)
		         {
		             chasing = false;
		         }
		
		         // attack, if close enough, and if time is OK:
		         if (distance <= attackThreshold && isLiving)
		         {
		             Attack();
		         }
		
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
    if (myColl.name == "BulletNeo(Clone)")
    {	
		StartCoroutine("Flash");
        health -= bulletDamage;
        if (health <= 0)
        {
			animation.Play("Death");	
            isLiving = false;
        }

        myColl.GetComponent<BulletScript>().RemoveMe();
        
    }
		
	if (myColl.name == "AttackRange" && playerTwo.GetComponent<PlayerTwoController>().animation["Rush"].enabled)
    {
        StartCoroutine("Flash");
        health -= 100000000;
        if (health <= 0)
        {
			animation.Play("Death");
            isLiving = false;
        }
    }	
		
		
	if (myColl.name	 == "ChargeShot2(Clone)")
	{
		health-= chargeShotDamage;
			
		if (health <= 0)
        {
			animation.Play("Death");	
          	isLiving = false;
        }
	}


    if (myColl.name == "GroundSlam(Clone)")
    {
        health -= chargeShotDamage;

        if (health <= 0)
        {
			animation.Play("Death");
            isLiving = false;
        }
    }

    if (myColl.name == "WhipTest(Clone)")
    {
        Vector3 posToGo = (playerTwo.transform.position);	
        transform.position = posToGo;	
        transform.Translate(Vector3.forward *5,Space.World);

        if (health <= 0)
        {
			animation.Play("Death");	
            isLiving = false;
        }
		myColl.GetComponent<WhipTestScript>().RemoveMe();
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
		
	if(myColl.name =="OilSpill2(Clone)" && slowed == false)
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

void OnCollisionEnter(Collision collision)
{
    
        Debug.Log("HIT");

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
		
	if (myColl.name == "AttackRange" && playerTwo.GetComponent<PlayerTwoController>().animation["AttackOne"].enabled)
    {
        StartCoroutine("Flash");
        health -= .5f;
        if (health <= 0)
        {
			animation.Play("Death");	
            isLiving = false;
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
	void Attack()
	{	
	    if( attackTimer <= 0)
		{
			animation.Play("Attack");
            soundManager.GetComponent<SoundManager>().PlaySound(4);
            if (target == playerOne)
            {
				
                target.GetComponent<PlayerOneController>().TakeDamageFromBasicEnemy();
                attackTimer = coolDown;
            }

            else if (target == playerTwo)
            {
                target.GetComponent<PlayerTwoController>().TakeDamageFromBasicEnemy();
                attackTimer = coolDown;
            }
		}
		
	}
	
}
	


