using UnityEngine;
using System.Collections;

public class TriggerEnemyManager : MonoBehaviour {


    public GameObject[] spawnPoints = new GameObject[0];
    private bool releaseEnemy = true;


    public void ImBeingChasedByZombies()
    {
        if (releaseEnemy)
        {
            int count = spawnPoints.Length;

            for (int i = 0; i < count; i++)
            {
                spawnPoints[i].GetComponent<TriggerEnemy>().releaseTheKrakken();
            }
            releaseEnemy = false; 
        }
    }
}
