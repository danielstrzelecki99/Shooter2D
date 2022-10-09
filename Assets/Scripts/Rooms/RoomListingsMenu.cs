using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomListingsMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Transform content;
    [SerializeField] RoomListing roomListing;

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo roomInfo in roomList)
        {
            RoomListing listing = Instantiate(roomListing, content);
            if (null != listing)
            {
                listing.setRoomInfo(roomInfo);
            }
        }
    }
}
