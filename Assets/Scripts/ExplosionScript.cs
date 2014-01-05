using UnityEngine;
using System.Collections;

public class ExplosionScript : MonoBehaviour 
{

    private float timeAlive = 0.0f;
    public float timeToDie = 1.0f;

    // Use this for initialization
    void Start()
    {
		GetComponent<AudioSource>().Play();
        gameObject.transform.renderer.material.color = new Color(1, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        timeAlive += Time.deltaTime;
        if (timeAlive > timeToDie)
        {
            RemoveMe();
        }
    }
    public void RemoveMe()
    {
        Destroy(gameObject);
    }

}
