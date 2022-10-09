using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomListing : MonoBehaviour
{
    [SerializeField]
    private Text _text;

    public void setRoomInfo(RoomInfo roomInfo)
    {
        Debug.Log(roomInfo.Name);
        _text.text = roomInfo.MaxPlayers + ", " + roomInfo.Name;
    }
}
