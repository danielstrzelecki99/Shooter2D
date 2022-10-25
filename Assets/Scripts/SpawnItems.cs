using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnItems : MonoBehaviour
{
    public List<GameObject> spawnObjects = new List<GameObject>();
    public List<Transform> spawnLocations = new List<Transform>();
    public bool stopSpawning = false;
    public float spawnTime;
    public float spawnDelay;
    public TextMeshProUGUI iteractInfo;
    [SerializeField] TextMeshProUGUI armorText;
    [SerializeField] TextMeshProUGUI ammoText; 
    [SerializeField] TextMeshProUGUI firstAidText;
   
    void Start()
    {
        InvokeRepeating("SpawnItem", spawnTime, spawnDelay);
    }

    void Update(){
        iteractInfo.text = PlayerMovement.interactInfoText;
        armorText.text = "Armors: " + PlayerEq.armorAmount.ToString();
        ammoText.text = "Ammo: " + PlayerEq.ammoAmount.ToString();
        firstAidText.text = "First aid kits: " + PlayerEq.aidKitAmount.ToString();
    }

    public void SpawnItem(){
        if(stopSpawning){
            CancelInvoke("SpawnItem");
        } else {
            GameObject itemToSpawn = spawnObjects[Random.Range(0,spawnObjects.Count)];
            Transform locationToSpawn = spawnLocations[Random.Range(0,spawnLocations.Count)];
            Instantiate(itemToSpawn, locationToSpawn);
            Debug.Log("Item spawned");
            spawnLocations.Remove(locationToSpawn);
        }
        if(spawnLocations.Count > 0){
            stopSpawning = false;
        }
        else if(spawnLocations.Count == 0){
            stopSpawning = true;
        }
        Debug.Log("Liczba miejsc do spawnowania: " + spawnLocations.Count);
    }
}
