using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItems : MonoBehaviour
{
    public GameObject[] spawnObjects;
    public Transform[] spawnLocations;
    public bool stopSpawning = false;
    public float spawnTime;
    public float spawnDelay;
   
    void Start()
    {
        InvokeRepeating("SpawnItem", spawnTime, spawnDelay);
    }

    public void SpawnItem(){
        Instantiate(spawnObjects[Random.Range(0, spawnObjects.Length)], spawnLocations[Random.Range(0,spawnLocations.Length)]);
        if(stopSpawning){
            CancelInvoke("SpawnItem");
            Debug.Log("Item spawned");
        }
    }
}
