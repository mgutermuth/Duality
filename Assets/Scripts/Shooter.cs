using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Shooter : MonoBehaviour
{

	public GameObject objBullet;
	public GameObject objCharge;
	public GameObject objRobit;
	public GameObject objSpill;
	public GameObject objShot;
    private GameObject[] robitsInPlay;
    private GameObject playerOne;
    public float fireRate = .5f;
    public float chargeTime = 1.0f;
    public float abilityOneCoolDown = 5.0f;
	public float abilityTwoCoolDown = 3.0f;
	private GameObject soundManager;
    [HideInInspector]
    public bool isCharging = false;
	public float timeCooledDown = 0.0f;
	public float timeCooledDown2 = 0.0f;
	private bool inLight;
    private int robits;
	public bool isRobitActive = false;
	private float nextFire = 0;
    public bool isPlayable;
	// Use this for initialization
	void Start () 
	{
        playerOne = GameObject.Find("PlayerOne");
		soundManager = GameObject.Find("SoundManager");
        isPlayable = playerOne.GetComponent<PlayerOneController>().isPlayable;
	}
	
	// Update is called once per frame
    void Update()
    {
		playerOne.animation["Charge"].speed = 1.25f;

        if (isPlayable)//not paused
        {
            
            robitsInPlay = GameObject.FindGameObjectsWithTag("Robit");
            inLight = playerOne.GetComponent<PlayerOneController>().inLight;
            foreach (GameObject robit in robitsInPlay)
            {

                if (robit.GetComponent<RobitController>().isMine == false)
                {
                    isRobitActive = true;
                    robit.transform.position = new Vector3(Mathf.Clamp(robit.transform.position.x, playerOne.transform.position.x - 30, playerOne.transform.position.x + 30),
                    3.0f, (Mathf.Clamp(robit.transform.position.z, playerOne.transform.position.z - 9, playerOne.transform.position.z + 20)));
                }
                else
                {
                    isRobitActive = false;
                }
            }
            if (robitsInPlay.Length < 1)
            {
                isRobitActive = false;
            }
            if (isCharging)
            {
                chargeTime -= Time.deltaTime;
            }

            if (chargeTime < 0)
            {
                LightAbilityOne();
            }

            if (timeCooledDown < 0)
            {
                timeCooledDown = 0;
            }
            if (timeCooledDown > 0)
            {
                timeCooledDown -= Time.deltaTime;
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
            if (Input.GetAxis("1ZTrig") < 0 && Time.time > nextFire && isRobitActive == false && isCharging == false)
            {
				if(!playerOne.animation["Run"].enabled)
				playerOne.animation.Play("Attack");
                Vector3 position = new Vector3(transform.position.x, 3.0f, transform.position.z);
                GameObject bullet = (GameObject)Instantiate(objBullet, position, transform.rotation);
                nextFire = Time.time + fireRate;
                soundManager.GetComponent<SoundManager>().PlaySound(0);
            }
            if (Input.GetButtonDown("1LBumper") && timeCooledDown == 0)
            {
                if (inLight == true)
                {
                    if (chargeTime > 0)
                    {
                        soundManager.GetComponent<SoundManager>().PlaySound(5);
						playerOne.animation.Play("Charge");
                        isCharging = true;
                    }
                }
                else if (inLight == false && isRobitActive == false)
                {
					playerOne.animation["Robit"].wrapMode = WrapMode.ClampForever;
					playerOne.animation.Play("Robit");
                    DarkAbilityOne();
                }
            }
            if (Input.GetAxis("1ZTrig") > 0 && timeCooledDown2 == 0 && isRobitActive == false && isCharging == false && !playerOne.animation["SWS"].enabled)
            {
                if (inLight == true)
                {
                    LightAbilityTwo();
                }
                else if (inLight == false)
                {
                    
                    playerOne.animation.Play("SWS");
                }
            }
			
			
			if(timeCooledDown2 == 0 && playerOne.animation["SWS"].time >= .34 )
			{
				DarkAbilityTwo();
			}
        }
    }
	

	
	void LightAbilityOne()
	{
		 
           // transform.Rotate(Vector3.right, 90.0f);
            Vector3 position  = new Vector3(transform.position.x , 2.5f, transform.position.z);
            Quaternion beamRotation = transform.rotation;
			 soundManager.GetComponent<SoundManager>().PlaySound(1);
            GameObject charge = (GameObject)Instantiate(objCharge, position, beamRotation);
            charge.transform.Translate(Vector3.forward *20, Space.Self);
            chargeTime = 1.0f;
            isCharging = false;
          //  transform.Rotate(Vector3.left, 90.0f);
			timeCooledDown = abilityOneCoolDown;
	}
	
	void DarkAbilityOne()
	{

        if (robitsInPlay.Length == 2)
        {
            robitsInPlay[0].GetComponent<RobitController>().Explode();
        }
		Vector3 position = new Vector3(transform.position.x, 3.5f, transform.position.z);
		GameObject robit = (GameObject)Instantiate(objRobit, position, transform.rotation);
	}
		
	void LightAbilityTwo()
	{	
		playerOne.animation.Play("JShot");
        soundManager.GetComponent<SoundManager>().PlaySound(7);
		Vector3 position = new Vector3 (playerOne.transform.position.x,0, playerOne.transform.position.z);
		transform.Rotate(Vector3.right, 90.0f);
		GameObject shot = (GameObject)Instantiate(objShot, position, transform.rotation);
        shot.transform.Translate(Vector3.up *5,Space.Self );
		shot.transform.Rotate( Vector3.left,90.0f);
		timeCooledDown2 = abilityTwoCoolDown;
		transform.Rotate(Vector3.left,90.0f);
	}
	
	
	
	void DarkAbilityTwo()
	{
		Vector3 position = new Vector3 (playerOne.transform.position.x,0, playerOne.transform.position.z);
		GameObject spill  =  (GameObject)Instantiate(objSpill,position,playerOne.GetComponent<CharacterController>().transform.rotation);
		timeCooledDown2 = abilityTwoCoolDown;	
	}
	
}
