using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class GetInformationToMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelInfo;
    [SerializeField] TextMeshProUGUI coinsInfo;
    [SerializeField] TextMeshProUGUI nameInfo;

    public void GetAccountInfo() {
        levelInfo.text = "Level: " + PlayFabManagerLogin.level.ToString();
        coinsInfo.text = "GIT coins: " + PlayFabManagerLogin.coins.ToString();
        nameInfo.text = "Welcome back " + PlayFabManagerLogin.username + "!";
    }

    void Start() {
        GetAccountInfo();
    }
}
