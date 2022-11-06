using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class LogoutMain : MonoBehaviour
{
    public void SetPlayerLoggedStatusToFalse(){
        var request = new UpdateUserDataRequest {
            Data = new Dictionary<string, string> {
                {"LoginStatus", "NotLogged"}
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
    }

    void OnDataSend(UpdateUserDataResult result){
        Debug.Log("User data updated");
    }

    void OnError(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }

    void OnApplicationQuit()
    {
        SetPlayerLoggedStatusToFalse();
    }
}
