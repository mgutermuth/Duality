using UnityEngine;
using System.Collections;

public class RestrictMovement : MonoBehaviour {

   public GameObject playerOne, playerTwo;
   public int xRestrict = 20;
   public int zRestrict = 30;

	// Use this for initialization
	void Start () {
       playerOne = GameObject.Find("PlayerOne");
       playerTwo = GameObject.Find("PlayerTwo");
	}
	


    public void RestrictPlayers(int PlayerNum)
    {
        if (playerOne.GetComponent<PlayerOneController>().isPlayable && playerTwo.GetComponent<PlayerTwoController>().isPlayable)
        {
            if (PlayerNum == 1)
            {
                playerOne.transform.position = new Vector3(Mathf.Clamp(playerOne.transform.position.x, playerTwo.transform.position.x - xRestrict, playerTwo.transform.position.x + xRestrict),
                    .6635f,
                    (Mathf.Clamp(playerOne.transform.position.z, playerTwo.transform.position.z - zRestrict, playerTwo.transform.position.z + zRestrict)));
            }
            else if (PlayerNum == 2)
            {
                playerTwo.transform.position = new Vector3(Mathf.Clamp(playerTwo.transform.position.x, playerOne.transform.position.x - xRestrict, playerOne.transform.position.x + xRestrict),
                      .6635f,
                     (Mathf.Clamp(playerTwo.transform.position.z, playerOne.transform.position.z - zRestrict, playerOne.transform.position.z + zRestrict)));
            }
        }
    }
}
