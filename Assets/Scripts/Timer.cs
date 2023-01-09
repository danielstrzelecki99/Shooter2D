using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class Timer : MonoBehaviour
{
    [SerializeField] private Image fill;
    [SerializeField] private TextMeshProUGUI text;
    public GameObject clockObject;
    public GameObject endGameUI;
    public TextMeshProUGUI endGameText;
    public GameObject quitGameUI;
    public Button quitButton;
    public Button yesButton;
    public Button noButton;
    public int duration;
    private int remainingDuration;
    public int currentLevel;
    public int currentExp;
    public int currentCoins;
    public bool leaveBeforeEnd = false;
    public bool quitUIShowed = false;
    //allow access the other classes without the reference
    public static Timer instance = null;
    public static bool isNoButtonPressed = false;

    private void Start()
    {
        instance = this;
        if (PhotonNetwork.CurrentRoom.CustomProperties["GameMode"].ToString() == "Deathmatch")
        {
            Being(duration);
            clockObject.transform.localScale = new Vector3(1, 1, 1); //show clock
        }
        else
        {
            clockObject.transform.localScale = new Vector3(0, 0, 0); //hide clock
        }
        noButton.onClick.AddListener(() => { hideQuitUI(); isNoButtonPressed = true;});
        yesButton.onClick.AddListener(() => { LeaveRoom(); });
    }

    public void Update(){
        if(Input.GetKeyDown(KeyCode.Escape) && quitUIShowed == false){
            showQuitUI();
            quitUIShowed = true;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && quitUIShowed == true){
            hideQuitUI();
            quitUIShowed = false;
        }
    }

    private void Being(int Second)
    {
        remainingDuration = Second;
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while(remainingDuration >= 0)
        {
            text.text = $"{remainingDuration / 60:00} : {remainingDuration % 60:00}";
            fill.fillAmount = Mathf.InverseLerp(0, duration, remainingDuration);
            remainingDuration--;
            if (remainingDuration <= 10){
                fill.color = Color.red;
            }
            yield return new WaitForSeconds(1f);
        }
        OnEnd();
    }

    public void OnEnd(bool win = false)
    {
        statisticUpdate();
        UpdatePlayerStatistics(win);
        PhotonNetwork.LeaveRoom();
        if (win)
        {
            endGameText.text = "Y O U\nW O N !!!";

        }
        endGameUI.SetActive(true);
        Invoke("nextScene", 4);
    }

    public void nextScene()
    {
        SceneManager.LoadScene("LoadingStats");
    }

    public void showQuitUI()
    {
        quitGameUI.SetActive(true);
    }

    public void hideQuitUI()
    {
        quitGameUI.SetActive(false);
        quitUIShowed = false;
    }

    public void LeaveRoom()
    {
        leaveBeforeEnd = true;
        if (PhotonNetwork.InRoom)
        {
            quitGameUI.SetActive(false);
            OnEnd();
        }
        else
        {
            OnEnd();
        }
    }

    public void statisticUpdate()
    {
        if(leaveBeforeEnd == false){
            currentExp = (int)(PlayFabManagerLogin.experience + PlayerEq.pointsInGame);
            currentCoins = (int)(PlayFabManagerLogin.coins + PlayerEq.pointsInGame/10);
        }
        else {
            currentExp = (int)(PlayFabManagerLogin.experience + (PlayerEq.pointsInGame/2));
            currentCoins = (int)(PlayFabManagerLogin.coins + ((PlayerEq.pointsInGame/10)/2));
        }
        if (currentExp >= 1000)
        {
            int levelToAdd = currentExp/1000;
            currentLevel = PlayFabManagerLogin.level + levelToAdd;
            currentExp = currentExp - 1000*levelToAdd;
        }
        else {currentLevel = PlayFabManagerLogin.level;
        }
    }

    public void UpdatePlayerStatistics(bool win){
        var wonGames = win ? PlayFabManagerLogin.wins + 1 : PlayFabManagerLogin.wins;
        var request = new UpdatePlayerStatisticsRequest {
            Statistics = new List<StatisticUpdate>{
                new StatisticUpdate {StatisticName = "PlayedGames", Value = PlayFabManagerLogin.playedGames + 1},
                new StatisticUpdate {StatisticName = "Experience", Value = currentExp},
                new StatisticUpdate {StatisticName = "Level", Value = currentLevel},
                new StatisticUpdate {StatisticName = "Coins", Value = currentCoins},
                new StatisticUpdate {StatisticName = "Wins", Value = wonGames}
                
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnUpdateStatistics, OnError);
    }

    void OnUpdateStatistics(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Statistics updated");
    }

    void OnError(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }
}
