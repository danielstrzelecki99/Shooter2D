using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class Logout : MonoBehaviour
{
    public Button logoutButton;

    public void SetPlayerLoggedStatusToFalse(){
        var request = new UpdatePlayerStatisticsRequest {
            Statistics = new List<StatisticUpdate>{
                new StatisticUpdate {StatisticName = "isLogged", Value = 0},
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnUpdateStatistics, OnError);
    }

    void OnUpdateStatistics(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Logged status has been updated");
    }

    void OnError(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }

    void Start()
    {
        logoutButton.onClick.AddListener(() => { SetPlayerLoggedStatusToFalse();});
    }

    void OnApplicationQuit()
    {
        SetPlayerLoggedStatusToFalse();
    }
}
