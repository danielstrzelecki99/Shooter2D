using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] private Image fill;
    [SerializeField] private TextMeshProUGUI text;

    public int duration;
    private int remainingDuration;

    private void Start()
    {
        Being(duration);
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
            yield return new WaitForSeconds(1f);
        }
        OnEnd();
    }

    private void OnEnd()
    {
        Debug.Log("Time ended");
        UpdatePlayerStatistics();
        SceneManager.LoadScene("Menu");
    }

    public void UpdatePlayerStatistics(){
        var request = new UpdatePlayerStatisticsRequest {
            Statistics = new List<StatisticUpdate>{
                new StatisticUpdate {StatisticName = "PlayedGames", Value = PlayFabManagerLogin.playedGames + 1},
                new StatisticUpdate {StatisticName = "Experience", Value = PlayFabManagerLogin.experience + PlayerEq.killsInGame},
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnUpdateStatistics, OnError);
    }

    void OnUpdateStatistics(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Statistics added");
    }

    void OnError(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }
}
