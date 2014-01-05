using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {
	
	
	public GameObject[] switchArray = new GameObject[2];
	public Vector3 direction = Vector3.left;
    public Material doorMatLeft;
    public Material doorMatRight;
    public Material doorMatActive;
    public Material doubleMatInactive;
    public Material singleDoorMatActive;
	public float distanceToMove = 3.0f;
	public float moveSpeed = 2.0f;
	private float distanceMoved = 0.0f;
	private bool doorIsGo1;
    private bool doorIsGo2;
    private bool doorIsGo;
	// Use this for initialization
	void Start ()
	{
		doorIsGo = false;
	}
	
	// Update is called once per frame
	void Update () 
	{

        //Left switch needs to be0, right switch needs to be 1

        if (!doorIsGo)
        {
            if (switchArray.Length > 1)
            {

                doorIsGo1 = switchArray[0].GetComponent<SwitchScript>().isActivated;
                doorIsGo2 = switchArray[1].GetComponent<SwitchScript>().isActivated;


                if (doorIsGo1 && doorIsGo2)
                {
                    doorIsGo = true;
                    this.renderer.material = doorMatActive;
                }

                else if (doorIsGo1 && !doorIsGo2)
                {
                    this.renderer.material = doorMatLeft;
                }
                else if (!doorIsGo1 && doorIsGo2)
                {
                    this.renderer.material = doorMatRight;
                }
                else if (!doorIsGo1 && !doorIsGo2)
                {
                    this.renderer.material = doubleMatInactive;
                }
            }
            else
            {
                doorIsGo = switchArray[0].GetComponent<SwitchScript>().isActivated;

                if (doorIsGo)
                {
                    this.renderer.material = singleDoorMatActive;
                }
            }
            
        }

        //if(!doorIsGo)
        //{
        //    for(int loop=0; loop < switchArray.Length; ++loop)
        //    {
        //        if(switchArray[loop].GetComponent<SwitchScript>().isActivated == false)
        //         { 
        //             doorIsGo = false;
        //             break;
        //         }
        //        else 
        //        {
        //            doorIsGo = true;
        //        }
        //    }
        //}
		if( doorIsGo == true)
		{
			
			//Destroy(gameObject);
			  if(Mathf.Abs(distanceMoved) < distanceToMove)
			{
				transform.Translate(direction *(Time.deltaTime* moveSpeed),Space.Self);
				distanceMoved += .01f;
			}
		}
	}
}
