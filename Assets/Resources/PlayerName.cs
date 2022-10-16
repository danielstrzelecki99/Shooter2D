using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class PlayerName : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameInfo;

    public void GetPlayerName() {
        nameInfo.text = PlayFabManagerLogin.username;
    }

    void Start()
    {
        GetPlayerName();
    }
}
