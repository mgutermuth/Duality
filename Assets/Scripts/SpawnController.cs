using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnController : MonoBehaviour {

    private GameObject[] Players = new GameObject[2];
    public GameObject theBaseEnemy;
    GameObject baseEnemy;


    public int baseEnemiesToSpawn = 4;
    public float DistanceToSpawn = 5;
    private int baseEnemiestoSpawnCounter = 0;

    private float spawnTime = 1.1f;

    private float nextSpawn = 0;

	// Use this for initialization
	void Start () {
        Players[0] = GameObject.Find("PlayerOne");
      Players[1] = GameObject.Find("PlayerTwo");
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Players[0] != null)
		{
            float disTwo = 0;
            if(Players[1].GetComponent<PlayerTwoController>().isPlayable)//if they're alive
                disTwo = Mathf.Abs((this.transform.position - Players[1].transform.position).magnitude);


        	float disOne = 0;
            if(Players[0].GetComponent<PlayerOneController>().isPlayable)
                 disOne = Mathf.Abs((this.transform.position - Players[0].transform.position).magnitude);

            float distanceOne;

            if (disOne >= disTwo)
                distanceOne = disOne;
            else
                distanceOne = disTwo;
		
      //  float distanceTwo = (this.transform.position - Players[1].transform.position).magnitude;

	        if (Time.time >= nextSpawn &&  baseEnemiestoSpawnCounter < baseEnemiesToSpawn && (distanceOne <= DistanceToSpawn))// || distanceTwo <= DistanceToSpawn))//players are close enough to spawn point.
	        {
	            baseEnemy = (GameObject) Instantiate(theBaseEnemy, this.transform.position, this.transform.rotation);
	            nextSpawn = Time.time + spawnTime;
	            baseEnemiestoSpawnCounter++;
	        }
		}
	
	}
}
