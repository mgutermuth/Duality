using UnityEngine;
using System.Collections;

public class GroundSlamScript : MonoBehaviour {

    private float timeAlive = 0.0f;
    public float timeToDie = 1.0f;


    // Use this for initialization
    void Start()
    {
        gameObject.transform.renderer.material.color = new Color(1.0f, .3f, .5f, .0f);
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
