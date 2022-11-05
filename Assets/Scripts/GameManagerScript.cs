using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Cinemachine;
using TMPro;
using CharacterCreator2D;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviourPun
{
    public GameObject playerPrefab;
    public CinemachineVirtualCamera myCinemachine;

    //respawn variables
    public TextMeshProUGUI spawnTimer;
    public GameObject respawnUI;
    private float TimeAmount = 4;
    private bool startRespawn;

    public TextMeshProUGUI pingrate; //variable to display player PingRate 
    //allow access the other classes without the reference
    public static GameManagerScript instance = null;
    //refence to LocalPlayer in PlayerMovement script
    [HideInInspector] public GameObject LocalPlayer;

    //Dispaly ammo variables
    public TextMeshProUGUI ammoText;

    public float[,] listOfSpawns =
    {
        {28, 0.2f},
        {37, 26},
        {24, 17},
        {5, 6},
        {2, -2},
        {47, 0.2f},
        {65, -2},
        {65, 9},
        {43, 14},
        {58, 20},
        {61.5f, 26}
    };

    public void Awake()
    {
        instance = this;
    }

    public void Update()
    {
        UpdateAmmoText();

        if (startRespawn)
        {
            StartRespawn();
        }
        pingrate.text = $"Ping: {PhotonNetwork.GetPing()}";
    }

    void StartRespawn()
    {
        //countdown timer
        TimeAmount -= Time.deltaTime;
        spawnTimer.text = "Respawn in: " + TimeAmount.ToString("F0");
        //check if counter is 0 or lower
        if(TimeAmount <= 0)
        {
            respawnUI.SetActive(false);
            startRespawn = false;
            //RelocatePlayer();
            //invoke method to enable inputs (move)
            Debug.Log(LocalPlayer);
            LocalPlayer.GetComponent<PlayerHealth>().EnableInputs();
            //invoke method Revive from playerHealth
            LocalPlayer.GetComponent<PhotonView>().RPC("Revive", RpcTarget.AllBuffered);
            SpawnAfterDeath();
        }
    }
    //enable the whole respawning system
    public void EnableRespawn()
    {
        TimeAmount = 4;
        startRespawn = true;
        respawnUI.SetActive(true);
    }
    public void LeaveRoom()
    {
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.LoadLevel("Menu");
        }
        else
        {
            SceneManager.LoadScene("Menu");
        }
    }
    public void SpawnAfterDeath()
    {
        System.Random gen = new System.Random();
        int numberOfSpawnPoint = gen.Next(11);
        Vector3 spawnPosition = new Vector3(listOfSpawns[numberOfSpawnPoint, 0], listOfSpawns[numberOfSpawnPoint, 1]);
        GameObject player;
        if (PhotonNetwork.InRoom)
        {
            player = PhotonNetwork.Instantiate(playerPrefab.name, spawnPosition, Quaternion.identity);
        }
        else
        {
            player = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
        }

        myCinemachine.Follow = player.transform;
    }
    public void RelocatePlayer()
    {
        System.Random gen = new System.Random();
        int numberOfSpawnPoint = gen.Next(11);
        Vector3 spawnPosition = new Vector3(listOfSpawns[numberOfSpawnPoint, 0], listOfSpawns[numberOfSpawnPoint, 1]);
        LocalPlayer.transform.localPosition = new Vector3(spawnPosition.x, spawnPosition.y, spawnPosition.z);
    }

    public void UpdateAmmoText()
    {
        if (WeaponManager.CurrentWeaponNo == 0) //if weapon is gun 
        {
            ammoText.text = $"{WeaponScript.RcurrentClip}/{"\u221E"}";
            //ammoText.text = $"{weaponController.AcurrentClip}/{"\u221E"}";
        }
        else //if weapon is riffle
        {
            ammoText.text = $"{WeaponScript.RcurrentClip}/{WeaponScript.RcurrentAmmo}";
            //ammoText.text = $"{weaponController.AcurrentClip}/{weaponController.AcurrentAmmo}";
        }
    }
}
