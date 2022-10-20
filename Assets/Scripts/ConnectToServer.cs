using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
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
            SceneManager.LoadScene("Menu");
        }
    }
}
