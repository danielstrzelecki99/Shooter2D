using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    private PlayFabManagerLogin managerLogin;
    void Start()
    {
        managerLogin = new PlayFabManagerLogin();
        managerLogin.GetUsername(); // when loading screen appers, we are sending request to database api
    }

    public void ConnectToServerWithSettings()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.NickName = PlayFabManagerLogin.username;
        var hash = PhotonNetwork.LocalPlayer.CustomProperties;
        hash["Level"] = PlayFabManagerLogin.level;
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        if (!PhotonNetwork.InLobby)
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
