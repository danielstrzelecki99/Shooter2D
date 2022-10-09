using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomListing : MonoBehaviour
{
    [SerializeField]
    private Text text;

    public void setRoomInfo(RoomInfo roomInfo)
    {
        text.text = roomInfo.MaxPlayers + ", " + roomInfo.Name;
    }
}
