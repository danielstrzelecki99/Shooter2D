using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;

public class PlayerName : MonoBehaviour
{
    [SerializeField] TextMeshPro nameInfo;

    public void GetPlayerName() {
        nameInfo.text = PhotonNetwork.NickName;
    }

    void Start()
    {
        GetPlayerName();
    }
}
