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

    public TextMeshProUGUI pingrate;

    public void Update()
    {
        pingrate.text = "NetworkPing: " + PhotonNetwork.GetPing();
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
