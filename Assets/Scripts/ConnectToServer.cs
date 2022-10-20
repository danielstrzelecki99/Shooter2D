using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    void Start()
    {
        Debug.Log(PlayFabManagerLogin.username);
        PhotonNetwork.NickName = PlayFabManagerLogin.username;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("polaczenie do mastera");
        if (!PhotonNetwork.InLobby)
        {
            Debug.Log("dolaczenie do lobby");
        }
    }

    public override void OnJoinedLobby()
    {
        SceneManager.LoadScene("Menu");
    }
}
