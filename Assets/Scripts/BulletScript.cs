using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {
	private float timeAlive = 0.0f;
	public float timeToDie = 0.02f;
	public float bulletSpeed = 2;
	
	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		timeAlive += Time.deltaTime;
		if(timeAlive > timeToDie)
		{
			RemoveMe();
		}
		transform.Translate(Vector3.forward,Space.Self);
		//rigidbody.AddForce(transform.forward *-500);
	}
	public	void RemoveMe()
		{
			Destroy(gameObject);
		}


    void OnCollisionEnter(Collision Other)
    {
        RemoveMe();
    }
	
}
