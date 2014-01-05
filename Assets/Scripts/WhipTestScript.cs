using UnityEngine;
using System.Collections;

public class WhipTestScript : MonoBehaviour 
{

    private float timeAlive = 0.0f;
    public float timeToDie = 1.0f;


    // Use this for initialization
    void Start()
    {
        gameObject.transform.renderer.material.color = new Color(.2f, 1.0f, .5f, .0f);
		//transform.localScale = new Vector3(transform.localScale.x,0,transform.localScale.z);
    }

    // Update is called once per frame
    void Update()
    {
		timeAlive += Time.deltaTime;
		if(timeAlive > timeToDie)
		{
			RemoveMe();
		}
		transform.Translate(Vector3.up,Space.Self);
	}
    public void RemoveMe()
    {
        Destroy(gameObject);
    }

}