using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Cinemachine;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
    public GameObject playerPrefab;
    public CinemachineVirtualCamera myCinemachine;

    public TextMeshProUGUI spawnTimer;
    public GameObject respawnUI;
    private float TimeAmount = 5;
    private bool startRespawn;

    public TextMeshProUGUI pingrate;
    //allow access the other classes without the reference
    public static GameManagerScript instance = null;
    //refence to LocalPlayer in PlayerMovement script
    [HideInInspector] public GameObject LocalPlayer;

    public SpawnPlayers spawnPlayer;


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
        if (startRespawn)
        {
            StartRespawn();
        }
        pingrate.text = "NetworkPing: " + PhotonNetwork.GetPing();
    }

    void StartRespawn()
    {
        TimeAmount -= Time.deltaTime;
        spawnTimer.text = "Respawn in: " + TimeAmount.ToString("F0");

        if(TimeAmount <= 0)
        {
            respawnUI.SetActive(false);
            startRespawn = false;
            //invoke method to enable inputs (move)
            Debug.Log(LocalPlayer);
            LocalPlayer.GetComponent<PlayerHealth>().EnableInputs();
            //invoke method Revive from playerHealth
            LocalPlayer.GetComponent<PhotonView>().RPC("Revive", RpcTarget.AllBuffered);
            Debug.Log("StartRespawn method invoked");
            SpawnAfterDeath();
        }
    }
    public void EnableRespawn()
    {
        TimeAmount = 5;
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
}
