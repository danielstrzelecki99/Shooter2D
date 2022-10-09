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

    private List<RoomListing> roomListings = new List<RoomListing>();

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo roomInfo in roomList)
        {
            if (roomInfo.RemovedFromList)
            {
                int index = roomListings.FindIndex(x => x.RoomInfo.Name == roomInfo.Name);
            }
            else
            {
                RoomListing listing = Instantiate(roomListing, content);
                if (null != listing)
                {
                    listing.setRoomInfo(roomInfo);
                    roomListings.Add(listing);
                }
            }
        }
    }
}

// https://www.youtube.com/watch?v=AbGwORylKqo&list=PLkx8oFug638oMagBH2qj1fXOkvBr6nhzt&index=8&ab_channel=FirstGearGames
