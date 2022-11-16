using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEditor;
using Photon.Pun;

public class GetInformationToMenu : MonoBehaviourPunCallbacks
{
    //Menu
    [SerializeField] TextMeshProUGUI levelInfo;
    [SerializeField] TextMeshProUGUI coinsInfo;
    [SerializeField] TextMeshProUGUI nameInfo;
    [SerializeField] TextMeshProUGUI expInfo;
    [SerializeField] Image fillLevelImage;

    //Statistics
    [SerializeField] TextMeshProUGUI levelStat;
    [SerializeField] TextMeshProUGUI winsStat;
    [SerializeField] TextMeshProUGUI gamesStat;
    [SerializeField] TextMeshProUGUI coinsStat;

    public void GetAccountInfo() {
        levelInfo.text = PlayFabManagerLogin.level.ToString();
        coinsInfo.text = "GIT coins: " + PlayFabManagerLogin.coins.ToString();
        nameInfo.text = "Welcome back " + PlayFabManagerLogin.username + "!";
        expInfo.text = PlayFabManagerLogin.experience.ToString() + "/1000";
        fillLevelImage.fillAmount = (float)((float)PlayFabManagerLogin.experience / 1000f);
        Debug.Log("Got Info!");
    }

    public void GetAccountStats() {
        levelStat.text = "Account level: " + PlayFabManagerLogin.level.ToString();
        winsStat.text = "Won games: " + PlayFabManagerLogin.wins.ToString();
        gamesStat.text = "Played games: " + PlayFabManagerLogin.playedGames.ToString();
        coinsStat.text = "GIT coins: " + PlayFabManagerLogin.coins.ToString();
        Debug.Log("Got Stats!");
    }

    void OnDataSend(UpdateUserDataResult result){
        Debug.Log("User data updated");
    }

     void OnError(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }

    public override void OnEnable()
    {
        base.OnEnable();
        GetAccountInfo();
        GetAccountStats();
    }
}
