using UnityEngine;
using System.Collections;

public class SwitchScript : MonoBehaviour 
{
    public Material activeSwitch;
    public Material inActiveSwitch;
    //public Material activeDoor;
    //public GameObject theDoor;
    public Light pointLight;
	[HideInInspector]
	public bool isActivated = false;
    public bool dirtyUpdate = true;
	public float timeToStayOn = 1.0f;
	private float timeStayedOn = 0.0f;
	[HideInInspector]
	//public bool isActivated = false;
	private GameObject playerTwo;
	private float oscillate = .01f;
	// Use this for initialization
	void Start () 	
	{
		playerTwo = GameObject.Find("PlayerTwo");
	
	}
	// Update is called once per frame
	void Update () 
	{
        if (dirtyUpdate)
        {
            if (isActivated)
            {
                this.renderer.material = activeSwitch;
               // theDoor.renderer.material = activeDoor;
                pointLight.intensity = 3;
                dirtyUpdate = false;

            }
        }


		float distance =  Mathf.Abs((transform.position - playerTwo.transform.position).magnitude);
		if( distance <= 10 && playerTwo.GetComponent<PlayerTwoController>().animation["AttackOne"].enabled)
		{
			isActivated = true;
		}
		
		if ( isActivated)
		{
			timeStayedOn += Time.deltaTime;
		}
		
		if (timeStayedOn >= timeToStayOn)
		{
			isActivated = false;
			timeStayedOn = 0;
            this.renderer.material = inActiveSwitch;
            dirtyUpdate = true;
		}
	}
	
	void OnTriggerEnter(Collider myColl)
	{
	    if (myColl.name == "BulletNeo(Clone)")
	    {	
			isActivated = true;	        
	    }
		if (myColl.name	 == "ChargeShot(Clone)")
		{
			isActivated = true;
		}
	
	    if (myColl.name == "Robit(Clone)")
	    {
			isActivated = true;
	        myColl.GetComponent<RobitController>().Explode();
	    }
	    if (myColl.name == "Explosion(Clone)")
	    {
			isActivated = true;
	    }
	
	    if (myColl.name == "JumpShot(Clone)")
	    {
			isActivated = true;
	    }
		if(myColl.name == "AttackRange" && playerTwo.GetComponent<PlayerTwoController>().animation["AttackOne"].enabled)
		{
			isActivated = true;
		}
		if(myColl.name == "GroundSlam(Clone)")
		{
			isActivated = true;	
		}
		if(myColl.name == "WhipTest(Clone)")
		{
			isActivated = true;
			myColl.GetComponent<WhipTestScript>().RemoveMe();
		}
	}
	
	void OnTriggerStay(Collider myColl)
	{
	   if (myColl.name == "AttackRange" && playerTwo.GetComponent<PlayerTwoController>().animation["AttackOne"].enabled)
    	{
			isActivated = true;       
    	}
	}
}
