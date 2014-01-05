using UnityEngine;
using System.Collections;

public class OilScript : MonoBehaviour {
	public float timeToDie = 10.0f;
	public float speedOfDecay = .09f;
    public Material fire;
    //public Component fire;
	[HideInInspector]
	public float timeAlive = 0.0f;
	public float oscillate = .0000001f;
	public bool isOnFire = false;
	private bool blarg = false;
	private bool shrink = false;
    private GameObject soundManager;

	// Use this for initialization
	void Start () 
	{

        soundManager = GameObject.Find("SoundManager");
       
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 temp = transform.position;
		timeAlive += Time.deltaTime;
		if(timeAlive > timeToDie)
		{
			shrink = true;
		}
		if(!blarg) // this whole blarg oscillate business is only intended as a temporary fix, I will have to look for a better one soon.
		{
			temp.x+= oscillate;
			transform.position = temp;
			
		}
		else
		{
			temp.x -= oscillate;
			transform.position = temp;
			
		}
		blarg = !blarg; // refer to the above blarg comment.	
		if( shrink )
		{
			Vector3 temp2 = transform.localScale;
			temp2.x -= speedOfDecay;
			temp2.z -= speedOfDecay;
			if(temp2.x < 0)
			{
				RemoveMe();
			}
			transform.localScale = temp2;
		}

        Debug.Log("FIRRREEE: " + isOnFire);
	}
	public	void RemoveMe()
		{
			Destroy(gameObject);
		}
	void OnTriggerStay(Collider myColl)
	{
		if(myColl.name == "Explosion(Clone)")
		{
			isOnFire = true;
            soundManager.GetComponent<SoundManager>().PlaySound(6);
            //GameObject Fire = (GameObject)Instantiate(fire, this.transform.position, transform.rotation);
            renderer.material = fire;
			
		}
	}
}
