using UnityEngine;
using System.Collections;

public class WaypointManager : MonoBehaviour {

	// Use this for initialization
    public GameObject[] wayPoints = new GameObject[0];
    private int CurrentActiveWP;
    public GameObject obelisk;
    public Material active;
    public Light pointLight;

	void Start () {
        wayPoints[0].GetComponent<Waypoint>().isActive = true;

        for (int index = 0; index < wayPoints.Length; index++)
        {
            wayPoints[index].GetComponent<Waypoint>().indexNumber = index;
        }
        
	
	}

    // Update is called once per frame
    void Update()
    {
	
	}

    public void HandleWP(int HitWP)
    {
        if (wayPoints[HitWP].GetComponent<Waypoint>().isActive != true)
        {
            for (int index = 0; index < wayPoints.Length; index++)
            {
                if (index != HitWP)
                    wayPoints[index].GetComponent<Waypoint>().isActive = false;
                else
                {
                    wayPoints[index].GetComponent<Waypoint>().isActive = true;
                    if (wayPoints[HitWP].GetComponent<Waypoint>().hasObelisk)
                    {
                        obelisk.renderer.material = active;
                        wayPoints[index].GetComponent<AudioSource>().Play();
                        pointLight.intensity = 3;

                    }
                }
            }
        }
    }

    public GameObject returnActiveWaypoint()
    {
        for (int i = 0; i < wayPoints.Length; i++)
        {
            if (wayPoints[i].GetComponent<Waypoint>().isActive == true)
                return wayPoints[i];
        }

        return null;
    }
}
