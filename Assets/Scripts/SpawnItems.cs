using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

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
    [SerializeField] TextMeshProUGUI itemInfo;
    PhotonView view;
   
    void Start()
    {
        InvokeRepeating("SpawnItem", spawnTime, spawnDelay);
    }

    void Update(){
            iteractInfo.text = PlayerMovement.interactInfoText;
            armorText.text = "[G] " + PlayerEq.armorAmount.ToString();
            ammoText.text = PlayerEq.ammoAmount.ToString();
            firstAidText.text = "[F] " +  PlayerEq.aidKitAmount.ToString();
            if(ItemsManager.interval > 0){
                itemInfo.text = ItemsManager.itemInfoText;
                ItemsManager.interval -= Time.deltaTime;
            } else {
                itemInfo.text = "";
            }
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
