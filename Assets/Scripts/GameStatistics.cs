using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using PlayFab;
using PlayFab.ClientModels;

public class GameStatistics : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI kills;
    [SerializeField] TextMeshProUGUI deaths;
    [SerializeField] TextMeshProUGUI damageDealt;
    [SerializeField] TextMeshProUGUI points;
    [SerializeField] TextMeshProUGUI level;
    [SerializeField] TextMeshProUGUI toNextLevel;
    [SerializeField] Image fillLevelImage;
    public float toFill;
    public Button switchToMenu;
    PhotonView view;
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
        Debug.Log(PlayFabManagerLogin.level);
        ShowStatistics();
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
        Debug.Log("Wyświetlenie danych");
        kills.text = "Kills: " + PlayerEq.killsInGame.ToString();
        deaths.text = "Deaths: " + PlayerEq.deathsInGame.ToString();
        damageDealt.text = "Damage dealt to enemy: " + PlayerEq.damageDealtInGame.ToString();
        points.text = "Points: " + PlayerEq.pointsInGame.ToString();
        level.text = PlayFabManagerLogin.level.ToString();
        toFill = (float)((float)PlayFabManagerLogin.experience / 1000f);
        toNextLevel.text = PlayFabManagerLogin.experience.ToString() + " / 1000";
        fillLevelImage.fillAmount = toFill;
        Debug.Log("Fill image: " + toFill);
        Debug.Log("Exp: " + PlayFabManagerLogin.experience);
    }
}
