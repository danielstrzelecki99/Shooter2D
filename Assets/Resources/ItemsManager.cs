using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemsManager : MonoBehaviourPun
{
    public static GameObject selectedObject;
    public static string itemInfoText;
    public static float interval;
    public bool stopSpawning = false;
    public float spawnTime;
    public float spawnDelay;
    public List<GameObject> spawnObjects = new List<GameObject>();
    public List<Transform> spawnLocations = new List<Transform>();

    PhotonView view;

    [SerializeField] private GameObject weapon;
    WeaponScript weaponController;

    public static bool isAmmoPickup= false;

    void Start()
    {
        Physics2D.IgnoreLayerCollision(9, 8);
        InvokeRepeating("SpawnItem", spawnTime, spawnDelay);
        view = GetComponent<PhotonView>();
        weaponController = weapon.GetComponent<WeaponScript>();
    }

    void Update()
    {
        if (PlayerEq.destroy == true)
        {
            PickUpCheck();
            PlayerEq.destroy = false;
        }       
    }

    public void PickUpCheck()
    {   
        if(selectedObject.tag == "AidKit"){
            if(PlayerEq.aidKitAmount == 2){
                itemInfoText = "You already have 2 first aid kits!";
                interval = 2f;
                return;
            }
            else{
                PlayerEq.aidKitAmount += 1;
            }
        } else if(selectedObject.tag == "AmmoKit"){
            if(PlayerEq.ammoAmount == 2){
                itemInfoText = "You already have 2 ammo packs!";
                interval = 2f;
                return;
            }
            else{
                isAmmoPickup = true;
                //PlayerEq.ammoAmount += 1;
                Debug.Log("Adding ammo to riffle");
                //weaponController.currentAmmo += 20;
            }
        } else if (selectedObject.tag == "Armor"){
            if(PlayerEq.armorAmount == 2){
                itemInfoText = "You already have 2 armors!";
                interval = 2f;
                return;
            }
            else{
                PlayerEq.armorAmount += 1;
            }
        }
        view.RPC("RPC_PickUp", RpcTarget.All);
    }

    [PunRPC]
    public void RPC_PickUp()
    {
        Destroy(selectedObject);
    }

    public void SpawnItem()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (stopSpawning)
            {
                CancelInvoke("SpawnItem");
            }
            else
            {
                int spawnObject = Random.Range(0, spawnObjects.Count);
                int spawnPosition = Random.Range(0, spawnLocations.Count);
                view.RPC("RPC_SpawnItem", RpcTarget.All, spawnObject, spawnPosition);
            }
            if (spawnLocations.Count > 0)
            {
                stopSpawning = false;
            }
            else if (spawnLocations.Count == 0)
            {
                stopSpawning = true;
            }
        }
    }

    [PunRPC]
    public void RPC_SpawnItem(int spawnObject, int spawnPosition)
    {
        GameObject itemToSpawn = spawnObjects[spawnObject];
        Transform locationToSpawn = spawnLocations[spawnPosition];
        Instantiate(itemToSpawn, locationToSpawn);
        spawnLocations.Remove(locationToSpawn);
    }
}
