using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomListing : MonoBehaviour
{
    [SerializeField]
    private Text _text;
    private bool _isPrivate;
    private CreateOrJoinCanvas createOrJoinCanvas;
    public GameObject lockImage;

    public RoomInfo RoomInfo { get; private set; }

    private void Awake()
    {
        createOrJoinCanvas = GetComponentInParent<CreateOrJoinCanvas>();
    }

    public void setRoomInfo(RoomInfo roomInfo)
    {
        RoomInfo = roomInfo;
        _isPrivate = (bool)roomInfo.CustomProperties["isPrivate"];
        _text.text = roomInfo.MaxPlayers + ", " + roomInfo.Name;
        if(_isPrivate)
            lockImage.SetActive(true);
    }

    public void OnClick_Button()
    {
        if (_isPrivate)
        {
            createOrJoinCanvas.EnterPasswordWindowShow();
            createOrJoinCanvas.room = RoomInfo;
        }
        else PhotonNetwork.JoinRoom(RoomInfo.Name);
    }
}
