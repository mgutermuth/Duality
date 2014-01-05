using UnityEngine;
using System.Collections;

public class Shooter2 : MonoBehaviour
{


    public GameObject objBullet;
    public GameObject objCharge;
    public GameObject objRobit;
    private GameObject[] robitsInPlay;
    private GameObject playerTwo;
    public float fireRate = .5f;
    public float chargeTime = 1.0f;
    public float abilityOneCoolDown = 5.0f;
    [HideInInspector]
    public bool isCharging = false;
    public float timeCooledDown = 0.0f;
    private bool inLight;
    private int robits;
    public bool isRobitActive = false;
    private float nextFire = 0;
    // Use this for initialization
    void Start()
    {
        playerTwo = GameObject.Find("PlayerTwo");
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetAxis("ShootTwo") < 0 && Time.time > nextFire && isRobitActive == false && isCharging == false)
        {
            Vector3 position = new Vector3(transform.position.x, 3.0f, transform.position.z);
            GameObject bullet = (GameObject)Instantiate(objBullet, position, transform.rotation);
            nextFire = Time.time + fireRate;
            GetComponent<AudioSource>().Play();
        }
      

    }


}
