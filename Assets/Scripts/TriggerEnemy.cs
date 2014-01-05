using UnityEngine;
using System.Collections;

public class TriggerEnemy : MonoBehaviour {


    public GameObject theBaseEnemy;
    private GameObject baseEnemy;


    public void releaseTheKrakken()
    {
        baseEnemy = (GameObject)Instantiate(theBaseEnemy, this.transform.position, this.transform.rotation);
    }
}
