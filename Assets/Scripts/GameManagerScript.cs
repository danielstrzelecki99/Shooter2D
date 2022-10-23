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
            LocalPlayer.GetComponent<PlayerHealth>().EnableInputs();
            //invoke method Revive from playerHealth
            LocalPlayer.GetComponent<PhotonView>().RPC("Revive", RpcTarget.AllBuffered);
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
}
