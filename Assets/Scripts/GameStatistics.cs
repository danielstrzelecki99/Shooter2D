using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using PlayFab;
using PlayFab.ClientModels;

public class GameStatistics : MonoBehaviourPunCallbacks
{
    [SerializeField] TextMeshProUGUI kills;
    [SerializeField] TextMeshProUGUI deaths;
    [SerializeField] TextMeshProUGUI damageDealt;
    [SerializeField] TextMeshProUGUI points;
    [SerializeField] TextMeshProUGUI level;
    [SerializeField] TextMeshProUGUI toNextLevel;
    [SerializeField] TextMeshProUGUI coins;
    [SerializeField] Image fillLevelImage;
    public float toFill;
    public int earnedCoins;
    public Button switchToMenu;
    PhotonView view;

    public override void OnEnable()
    {
        base.OnEnable();
        Cursor.visible = true;
    }

    void Start()
    {
            GetStatistics();
            view = GetComponent<PhotonView>();
            switchToMenu.onClick.AddListener(() => { SwitchToMenu();});
    }
    public void GetStatistics()
    {
        PlayFabClientAPI.GetPlayerStatistics(new GetPlayerStatisticsRequest(), OnStatisticsRecived, OnError);
    }

    void OnStatisticsRecived(GetPlayerStatisticsResult result)
    {
        Debug.Log("I got statistics!");
        foreach(var stat in result.Statistics){
            if(stat.StatisticName == "Level"){
                PlayFabManagerLogin.level = stat.Value;
            }
            if(stat.StatisticName == "Experience"){
                PlayFabManagerLogin.experience = stat.Value;
            }
            if(stat.StatisticName == "Coins"){
                PlayFabManagerLogin.coins = stat.Value;
            }
            if(stat.StatisticName == "PlayedGames"){
                PlayFabManagerLogin.playedGames = stat.Value;
            }
            if(stat.StatisticName == "Wins"){
                PlayFabManagerLogin.wins = stat.Value;
            }
        }
        Invoke("ShowStatistics", 1);
    }
    void OnError(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }
    public void SwitchToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ShowStatistics()
    {
        kills.text = "Kills: " + PlayerEq.killsInGame.ToString();
        deaths.text = "Deaths: " + PlayerEq.deathsInGame.ToString();
        damageDealt.text = "Damage dealt to enemy: " + PlayerEq.damageDealtInGame.ToString();
        points.text = "Points: " + PlayerEq.pointsInGame.ToString();
        level.text = PlayFabManagerLogin.level.ToString();
        earnedCoins = (int)(PlayerEq.pointsInGame/10);
        coins.text = "Earned coins: " + earnedCoins.ToString();
        toFill = (float)((float)PlayFabManagerLogin.experience / 1000f);
        toNextLevel.text = PlayFabManagerLogin.experience.ToString() + " / 1000";
        fillLevelImage.fillAmount = toFill;
    }
}
