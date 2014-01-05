using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RobitController : MonoBehaviour {

    private Vector3 playerInput;
    CharacterController controller;
    Vector3 rotation = Vector3.zero;
    public GameObject objExplosion;
    private bool isAlive = true;
    public float moveSpeed = .75f;
	//private GameObject soundManager;
    [HideInInspector]
    public bool isMine = false;
	// Use this for initialization
	void Start ()
    {
	//	soundManager = GameObject.Find("SoundManager");
        controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if(isMine != true)
        {
            playerInput = new Vector3(Input.GetAxis("1Horizontal"), 0, Input.GetAxis("1Vertical"));//movement
            controller.Move(playerInput * moveSpeed);
            GameObject.Find("PlayerOne").GetComponentInChildren<Shooter>().timeCooledDown = GameObject.Find("PlayerOne").GetComponentInChildren<Shooter>().abilityOneCoolDown;
        }


        if (Input.GetButtonDown("1LBumper"))
        {
            isMine = true;   
        }

	
	}

 public void Explode()
    {
		GameObject.Find("PlayerOne").animation["Robit"].wrapMode = WrapMode.Once;
        GameObject explosion = (GameObject)Instantiate(objExplosion, transform.position, transform.rotation);
		 //soundManager.GetComponent<SoundManager>().PlaySound(3);
        Destroy(gameObject);
    }
        
}
