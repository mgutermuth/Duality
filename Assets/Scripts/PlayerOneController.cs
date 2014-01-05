using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class PlayerOneController : MonoBehaviour {

 
    public float maxHealth;
	public float jumpShotDistancePerTick = 1.0f;
    private GameObject[] robits;
    CharacterController controller;
    Vector3 rotation = Vector3.zero;
    public GameObject obj;
    public float health;
	public bool isAlive = true;
	public bool inLight = false;
	public float moveSpeed = 1.5f;
	public float damageFromBasicEnemy = 10.0f;
    public bool isPlayable = true;
	private bool isRunning = false;
    GameObject shooterz;
	private Vector3 playa = Vector3.zero;
	private Vector3 rotatoe  = Vector3.zero;
	private GameObject playerTwo;
	private GameObject cam;

	// Use this for initialization
	void Start () 
	{
        health = maxHealth;
        controller = GetComponent<CharacterController>();
       // obj = GameObject.FindGameObjectWithTag("EditorOnly");
        shooterz = GameObject.Find("shooter");
		health = maxHealth;
		playerTwo = GameObject.Find("PlayerTwo");
		cam = GameObject.Find("Main Camera");
	}
	
	// Update is called once per frame
    void Update()
    {
        if (isPlayable)
        {
            if (isRunning == false && !animation["Robit"].enabled && !animation["Charge"].enabled && !animation["Attack"].enabled && !animation["SWS"].enabled && !animation["JShot"].enabled)
            {
                animation.Play("Idle");
            }
				
            playa = (new Vector3(Input.GetAxis("1Horizontal"), 0, Input.GetAxis("1Vertical")));//movement
            rotatoe = (new Vector3(Input.GetAxis("1RTX"), 0, Input.GetAxis("1RTY")));//rotation

            if (!gameObject.GetComponentInChildren<Shooter>().isCharging && !GetComponentInChildren<Shooter>().isRobitActive)//move around if robot isnt out our shooter charging.
            {
                controller.Move(playa * moveSpeed);
                if (playa != Vector3.zero)
                {
                    animation.Play("Run");
                    isRunning = true;
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
                    controller.transform.rotation = Quaternion.LookRotation(rotatoe);

                }
               obj.GetComponent<RestrictMovement>().RestrictPlayers(1);

            }

            if (animation["JShot"].enabled)
            {
                controller.Move(rotation * -jumpShotDistancePerTick);
            }

            if (!isAlive)//IF THEY DIE, GET IT!?!?!?!?
            {
                isPlayable = false;
                GameObject waypointTemp = obj.GetComponent<WaypointManager>().returnActiveWaypoint();
                if (!playerTwo.GetComponent<PlayerTwoController>().isPlayable)
                {
                    gameObject.transform.position = new Vector3(waypointTemp.transform.position.x - 3, -0.2342463f, waypointTemp.transform.position.z);
                    playerTwo.GetComponent<PlayerTwoController>().transform.position = new Vector3(waypointTemp.transform.position.x + 3, -0.2342463f, waypointTemp.transform.position.z);
                    playerTwo.GetComponent<PlayerTwoController>().bringToLife();
                    isPlayable = true;
                    isAlive = true;
                }
                else
                {
                    gameObject.transform.position = new Vector3(waypointTemp.transform.position.x - 3, -100.2342463f, waypointTemp.transform.position.z);

                }
            }
            if (playa == Vector3.zero)
            {
                isRunning = false;
            }

          //  this.transform.position = new Vector3(Mathf.Clamp(this.transform.position.x, playerTwo.transform.position.x - 50, playerTwo.transform.position.x + 50),
                                      //  .6635f,
                                     //    (Mathf.Clamp(this.transform.position.z, playerTwo.transform.position.z - 50, playerTwo.transform.position.z + 50)));
        }
    }


    void OnTriggerEnter(Collider myCol)
    {

        if (myCol.name == "Waypoint")
        {
            obj.GetComponent<WaypointManager>().HandleWP(myCol.gameObject.GetComponent<Waypoint>().indexNumber);
            if (!playerTwo.GetComponent<PlayerTwoController>().isPlayable)//hit waypoint and player 2 isnt alive
            {

                GameObject waypointTemp = obj.GetComponent<WaypointManager>().returnActiveWaypoint();
                playerTwo.GetComponent<PlayerTwoController>().transform.position = new Vector3(waypointTemp.transform.position.x + 3, -0.2342463f, waypointTemp.transform.position.z);
                playerTwo.GetComponent<PlayerTwoController>().bringToLife();
            }
        }
        else if (myCol.name == "AudioCube")
        {
            if (myCol.GetComponent<AudioLawl>().hasPlayed == false)
            {
                myCol.GetComponent<AudioLawl>().hasPlayed = true;
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
	
	public void bringToLife()
	{
		isAlive = true;
		isPlayable = true;
		health = maxHealth;
	}
	
    public void TakeDamageFromBasicEnemy()
	{
		health -= damageFromBasicEnemy;
		StartCoroutine("Flash");
		if( health <= 0)
		{
			isAlive = false;
		}	
	}
	
	IEnumerator Flash ()
	{
		 gameObject.GetComponentInChildren<Renderer>().material.color  = Color.red;
		yield return new WaitForSeconds (.1f);
        gameObject.GetComponentInChildren<Renderer>().material.color = Color.white;
	}
	
}
	




