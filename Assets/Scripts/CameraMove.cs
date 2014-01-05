using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraMove : MonoBehaviour {



    public float minDistance, maxDistance;
    public float ViewportPadding = .2f;

    private float viewportMultiplier;

	private float largeX, smallX, Distance;
	private int smallIndex, largeIndex;
    private float startingZPosition;
    private float FOVtoZoom;
    public float testingVar;


    private GameObject playerOne, playerTwo;



	// Use this for initialization
	void Start () {

        playerOne = GameObject.Find("PlayerOne");
        playerTwo = GameObject.Find("PlayerTwo");

         startingZPosition = this.transform.position.z;        
        FOVtoZoom = startingZPosition / this.camera.fieldOfView;
        viewportMultiplier = ViewportPadding +1;


	}
	
	// Update is called once per frame
	void Update () {

        Distance = Mathf.Clamp(((Mathf.Abs(playerOne.transform.position.x - playerTwo.transform.position.x)) * viewportMultiplier), minDistance, maxDistance) * FOVtoZoom;
        AverageCamera();            

	}



    void AverageCamera()
    {
		bool pOnePlay = playerOne.GetComponent<PlayerOneController>().isPlayable;
		bool pTwoPlay = playerTwo.GetComponent<PlayerTwoController>().isPlayable;
		
        float sumX = (pOnePlay ? playerOne.transform.position.x : 0) 
					+ (pTwoPlay ? playerTwo.transform.position.x : 0);
        float sumZ = (pOnePlay ? playerOne.transform.position.z : 0)
					+ (pTwoPlay ? playerTwo.transform.position.z : 0);



        float secondTest = Mathf.Clamp(Mathf.Abs(playerOne.transform.position.z - playerTwo.transform.position.z) * 1.2f, 10, testingVar);

        Vector3 averagepos;
        averagepos.x = sumX / ( (pOnePlay ? 1:0) + (pTwoPlay ? 1 : 0)  );
        averagepos.y = -Distance  + secondTest;// this.transform.position.y;
        averagepos.z = (sumZ / ((pOnePlay ? 1 : 0) + (pTwoPlay ? 1 : 0))) - secondTest;
        this.transform.position = averagepos;
        
    }
}



