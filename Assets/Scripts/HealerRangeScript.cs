using UnityEngine;
using System.Collections;

public class HealerRangeScript : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		 transform.renderer.material.color = new Color(.13f,.45f,.34f);
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	public void setRadius(float radius)
	{
		Vector3 temp = new Vector3 (radius,0,radius);
		transform.localScale = temp;
	}
}
