using UnityEngine;
using System.Collections;

public class Lights : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider myCollider)
    {
        if (myCollider.name == "Point light")
        {
			gameObject.GetComponent<PlayerOneController>().inLight = true;
		}
    }

    void OnTriggerExit(Collider myCollider)
    {
        if (myCollider.name == "Point light")
        {
				gameObject.GetComponent<PlayerOneController>().inLight = false;
        }
    }


}
