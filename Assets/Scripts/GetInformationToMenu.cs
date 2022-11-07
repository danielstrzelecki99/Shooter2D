using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class GetInformationToMenu : MonoBehaviour
{
    //Menu
    [SerializeField] TextMeshProUGUI levelInfo;
    [SerializeField] TextMeshProUGUI coinsInfo;
    [SerializeField] TextMeshProUGUI nameInfo;

    //Statistics
    [SerializeField] TextMeshProUGUI levelStat;
    [SerializeField] TextMeshProUGUI winsStat;
    [SerializeField] TextMeshProUGUI gamesStat;
    [SerializeField] TextMeshProUGUI coinsStat;

    public void GetAccountInfo() {
        levelInfo.text = "Level: " + PlayFabManagerLogin.level.ToString();
        coinsInfo.text = "GIT coins: " + PlayFabManagerLogin.coins.ToString();
        nameInfo.text = "Welcome back " + PlayFabManagerLogin.username + "!";
    }

    public void GetAccountStats() {
        levelStat.text = "Account level: " + PlayFabManagerLogin.level.ToString();
        winsStat.text = "Won games: " + PlayFabManagerLogin.wins.ToString();
        gamesStat.text = "Played games: " + PlayFabManagerLogin.playedGames.ToString();
        coinsStat.text = "GIT coins: " + PlayFabManagerLogin.coins.ToString();
    }

    //Setting account status
    public void SetPlayerLoggedStatusToTrue(){
        var request = new UpdateUserDataRequest {
            Data = new Dictionary<string, string> {
                {"LoginStatus", "Logged"}
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

    void Start() {
        GetAccountInfo();
        GetAccountStats();
        SetPlayerLoggedStatusToTrue();
    }
}
