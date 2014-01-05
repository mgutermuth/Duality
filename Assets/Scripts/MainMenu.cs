using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {




    public TextMesh playerOne;
    [HideInInspector]
    public int index = 0;
    private int WhichMenu = 0;
    private Camera MainCamera;
    private float input;
    private bool button = false;
    private bool button2 = false;


	// Use this for initialization
	void Start () {
        MainCamera = Camera.mainCamera;
        playerOne.text = " ";
	}
	
	// Update is called once per frame
	void Update () {

        input = Input.GetAxis("1Vertical");
        button = Input.GetButtonDown("1AButton");
        button2 = Input.GetButtonDown("1BButton");

        if (WhichMenu == 0)
        {


            if (input > .3f && index > 0)
            {
                index--;
                this.transform.position = new Vector3(-7, 9.3f, -41.5f);
            }
            else if (input < -.3f && index < 1)
            {
                index++;
                this.transform.position = new Vector3(-7, 8.1f, -41.5f);
            }

            if (button)
            {
                switch (index)
                {
                    case 0:
                        MainCamera.transform.position = new Vector3(22.8f, 14.3f, -44.4f);
                        WhichMenu = 1;
                        
                        break;
                    case 1:
                        Application.Quit();
                        Debug.Log("exit game");
                        break;
                }
            }
        }//whichMenu
        else if (WhichMenu == 1)
        {

            if (button2)//back button
            {
                MainCamera.transform.position = new Vector3(-8.31f, 10.45f, -49.48f);
                playerOne.text = " ";
                WhichMenu = 0;
                        
            }
            if (button)
            {
                playerOne.text = "Player One - Ready";
            }
        }

	}
}
