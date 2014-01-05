using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	// Use this for initialization


    public GameObject[] audioCue = new GameObject[0];


	void Start () {


        for (int index = 0; index < audioCue.Length; index++)
        {
        //    audioCue[index].GetComponent<AudioLawl>().indexNumber = index;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void HandleAudio(int index)
    {

    }
}
