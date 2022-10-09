using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Cinemachine;


public class GameManagerScript : MonoBehaviour
{
    public GameObject playerPrefab;
    public CinemachineVirtualCamera myCinemachine;

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
