using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
	public  AudioClip[] sounds;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	public void PlaySound(int id)
	{
		GetComponent<AudioSource>().PlayOneShot(sounds[id],.3f);
	}
	
}
