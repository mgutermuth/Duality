using UnityEngine;
using System.Collections;

public class SoundScript : MonoBehaviour
{
	 
	 private bool hasPlayed = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void onTriggerEnter(Collider myColl)
	{
		if( myColl.tag == "Player" && hasPlayed == false)
		{
			Debug.Log("HERE");
			hasPlayed = false;
			GetComponent<AudioSource>().Play();
		}
		
	}
}
