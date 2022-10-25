using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;

public class PlayerName : MonoBehaviour
{
    [SerializeField] TextMeshPro nameInfo;

    PhotonView view;

    [PunRPC]
    public void GetPlayerName() 
    {
        nameInfo.text = view.Owner.NickName;
    }

    void Start()
    {
        view = GetComponent<PhotonView>();
        GetPlayerName();
    }
}
