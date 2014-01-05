using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerTwoController : MonoBehaviour {
  //  private List<Vector3> playerInput = new List<Vector3>();
    private GameObject[] robits;
	public GameObject objWhip;
    public GameObject objGroundSlam;
    public float timeToShield = 10.0f;
    CharacterController controller;
    Vector3 rotation = Vector3.zero;
    public GameObject obj;
	private GameObject playerOne;
    public float  maxHealth;
    public float health;
    private bool isAlive = true;
    public bool inLight = false;
    private bool robitActive = false;
    private float soundTimer = 0;
    public float moveSpeed = .5f;
    public float damageFromBasicEnemy = 10.0f;
    public float abilityOneCoolDown = 5.0f;
    public float abilityTwoCoolDown = 3.0f;
    public float stayInAnimation = .5f;
    public float stayedInAnimation = 0.0f;
    public float defenseIncrease = 2.0f;
    private float originalDamage; 
	[HideInInspector]
    public float stayedInShield = 0.0f;
    public float timeCooledDown = 0.0f;
    public float timeCooledDown2 = 0.0f;
	public bool isRunning;
    public float AttackFrame = 0;
    private GameObject soundManager;
	public bool isPlayable = true;
	private Vector3 playa = Vector3.zero;
	private Vector3 rotatoe = Vector3.zero;
	public float multiplyer = 9.0f;
    // Use this for initialization
    void Start()
    {
        health = maxHealth;
        originalDamage = damageFromBasicEnemy;
		
        playerOne = GameObject.Find("PlayerOne");
        controller = GetComponent<CharacterController>();
        soundManager = GameObject.Find("SoundManager");
		
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(soundManager.GetComponent<SoundManager>().sounds[3].length);
		if(isPlayable)
		{
        if (!isRunning && !animation["AttackOne"].enabled && !animation["Whip"].enabled && !animation["Rush"].enabled && !animation["GroundSlam"].enabled && !animation["Shield"].enabled)
		{
			animation.Play("Idle");
		}
		if (isRunning)
		{
			animation.Play("Run");
		}
        if (inLight)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.blue;
        }
        else
        {
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }



        if (timeCooledDown < 0)
        {
            timeCooledDown = 0;
        }
        if (timeCooledDown > 0)
        {
            timeCooledDown -= Time.deltaTime;
        }


        if (stayedInAnimation < 0)
        {
            animation["Rush"].wrapMode = WrapMode.Once;
            stayedInAnimation = 0;
        }

        if (timeCooledDown < 0)
        {
            timeCooledDown = 0;
        }
        if (timeCooledDown2 > 0)
        {
            timeCooledDown2 -= Time.deltaTime;
        }

        if (timeCooledDown2 < 0)
        {
            timeCooledDown2 = 0;
        }

        if (stayedInShield > 0)
        {
            stayedInShield -= Time.deltaTime;
        }

        if (soundTimer > 0)
        {
            soundTimer -= Time.deltaTime;
        }
        if (stayedInShield < 0)
        {
            damageFromBasicEnemy = originalDamage;
            stayedInShield = 0;
        }

		

			playa = (new Vector3(Input.GetAxis("2Horizontal"), 0, Input.GetAxis("2Vertical")));
			rotatoe = (new Vector3(Input.GetAxis("2RTX"), 0, Input.GetAxis("2RTY")));//rotation

        if (!animation["AttackOne"].enabled && !animation["Whip"].enabled && !animation["Rush"].enabled && !animation["GroundSlam"].enabled && !animation["Shield"].enabled)
        {
            controller.Move(playa * moveSpeed);

            if (playa != Vector3.zero)
            {
                isRunning = true;
            }
        }

            if (rotatoe == Vector3.zero)//right stick not in use
            {
                if (playa == Vector3.zero)//left one isnt either
                    controller.transform.rotation = Quaternion.LookRotation(rotation);
                else
                    rotation = new Vector3(playa.x, 0.0f, playa.z);
                controller.transform.rotation = Quaternion.LookRotation(rotation);
            }
            else//right stick being used!
            {
                rotation = rotatoe;
                controller.transform.rotation = Quaternion.LookRotation(rotation);
            }

            if (stayedInAnimation > 0)
            {
                stayedInAnimation -= Time.deltaTime;
            }

           
          //  this.transform.position = new Vector3(Mathf.Clamp(this.transform.position.x, playerOne.transform.position.x - 50, playerOne.transform.position.x + 50),
            //         .6635f,
            //        (Mathf.Clamp(this.transform.position.z, playerOne.transform.position.z - 50, playerOne.transform.position.z + 50)));

		if(playa ==  Vector3.zero)
		{
			isRunning = false;
		}


        if (Input.GetAxis("2ZTrig") < 0 && !animation["Whip"].enabled && !animation["Rush"].enabled && !animation["GroundSlam"].enabled && !animation["Shield"].enabled)

        {
            playAttack();
        }


        if (Input.GetButtonDown("2LBumper") && timeCooledDown == 0 && !animation["Whip"].enabled && !animation["Rush"].enabled && !animation["GroundSlam"].enabled && !animation["Shield"].enabled)
        {
            if (inLight == true)
            {
                soundManager.GetComponent<SoundManager>().PlaySound(8);
                animation.Play("Whip");
            }
            else if (inLight == false)
            {
                DarkAbilityOne();
            }
        }

        if (Input.GetAxis("2ZTrig") > 0 && timeCooledDown2 == 0 && !animation["Whip"].enabled && !animation["Rush"].enabled && !animation["GroundSlam"].enabled && !animation["Shield"].enabled)
        {
            if (inLight == true)
            {
                LightAbilityTwo();
            }
            else if (inLight == false)
            {
				animation.Play("GroundSlam");
            }
        }
			if(!isAlive)//D E A D
			{
				isPlayable = false;
				GameObject waypointTemp = obj.GetComponent<WaypointManager>().returnActiveWaypoint();
				if(!playerOne.GetComponent<PlayerOneController>().isPlayable)
				{
					gameObject.transform.position = new Vector3(waypointTemp.transform.position.x - 3, -0.2342463f, waypointTemp.transform.position.z);
					playerOne.GetComponent<PlayerOneController>().transform.position = new Vector3 (waypointTemp.transform.position.x + 3, -0.2342463f, waypointTemp.transform.position.z);
					playerOne.GetComponent<PlayerOneController>().bringToLife();
					isPlayable = true;
					isAlive = true;
				}
				else
				{
					gameObject.transform.position = new Vector3(waypointTemp.transform.position.x - 3, -100.2342463f, waypointTemp.transform.position.z);
						
				}
			}
		}



        if (animation["Rush"].enabled)
        {
            controller.Move(rotation * 1.2f);
        }
		if( timeCooledDown2 == 0 && animation["GroundSlam"].time >= .49)
		{
			DarkAbilityTwo();
		}
		if( timeCooledDown == 0 && animation["Whip"].time >= .9)
		{
			LightAbilityOne();
		}
        obj.GetComponent<RestrictMovement>().RestrictPlayers(2);
    }


	public void bringToLife()
	{
		isAlive = true;
		isPlayable = true;
		health = maxHealth;
	}

    void OnTriggerEnter(Collider myCol)
    {

        if (myCol.name == "Waypoint")
        {
            obj.GetComponent<WaypointManager>().HandleWP(myCol.gameObject.GetComponent<Waypoint>().indexNumber);
            if (!playerOne.GetComponent<PlayerOneController>().isPlayable)
            {
                GameObject waypointTemp = obj.GetComponent<WaypointManager>().returnActiveWaypoint();
                playerOne.GetComponent<PlayerOneController>().transform.position = new Vector3(waypointTemp.transform.position.x + 3, -0.2342463f, waypointTemp.transform.position.z);
                playerOne.GetComponent<PlayerOneController>().bringToLife();
            }
        }
        else if (myCol.name == "AudioCube")
        {
            if (myCol.GetComponent<AudioLawl>().hasPlayed == false)
            {
                myCol.GetComponent<AudioLawl>().hasPlayed = true;
             //   myCol.GetComponent<AudioSource>().clip
                myCol.GetComponent<AudioSource>().Play();
            }
        }
        else if (myCol.name == "SpawnTrigger")
        {
            myCol.GetComponent<TriggerEnemyManager>().ImBeingChasedByZombies();
        }
        else if (myCol.name == "EndLevel")
        {
            obj.GetComponent<EndLevel>().loadLevel();
        }

    }



    public void TakeDamageFromBasicEnemy()
    {
        health -= damageFromBasicEnemy;
        StartCoroutine("Flash");
        if (health <= 0)
        {
            isAlive = false;
        }
    }


    void DarkAbilityTwo()
    {
        isRunning = false;
        soundManager.GetComponent<SoundManager>().PlaySound(11);
        GameObject groundSlam = (GameObject)Instantiate(objGroundSlam, transform.position, transform.rotation);
        timeCooledDown2 = abilityTwoCoolDown;
    }


    void LightAbilityTwo()
    {
        isRunning = false;
        animation.Play("Shield");
        soundManager.GetComponent<SoundManager>().PlaySound(12);
        damageFromBasicEnemy /= defenseIncrease;
        stayedInShield = timeToShield;
        timeCooledDown2 = abilityTwoCoolDown;
    }

    void LightAbilityOne()
    {
        isRunning = false;
        soundManager.GetComponent<SoundManager>().PlaySound(9);
        Vector3 position  = new Vector3(transform.position.x , 2.5f, transform.position.z);
        GameObject whip = (GameObject)Instantiate(objWhip,position ,transform.rotation);
        whip.transform.Rotate(Vector3.right, 90.0f);
        whip.transform.Translate(Vector2.right *2, Space.Self);
		timeCooledDown = abilityOneCoolDown;
    }


    void DarkAbilityOne()
    {
        isRunning = false;
        animation["Rush"].wrapMode = WrapMode.ClampForever;
        animation.Play("Rush");
        stayedInAnimation = stayInAnimation;
		timeCooledDown = abilityOneCoolDown;

    }

    IEnumerator Flash()
    {
        gameObject.GetComponentInChildren<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(.1f);
        gameObject.GetComponentInChildren<Renderer>().material.color = Color.white;
    }
    void playAttack()
    {
        if (soundTimer <= 0)
        {
            soundManager.GetComponent<SoundManager>().PlaySound(3);
            soundTimer = .491f;
        }
            isRunning = false;
            animation.Play("AttackOne");
    }

}

