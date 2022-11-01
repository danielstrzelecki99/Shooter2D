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

    public List<RoomListing> roomListings = new List<RoomListing>();
    private RoomsCanvases roomsCanvases;

    public void FirstInitialize(RoomsCanvases canvases)
    {
        roomsCanvases = canvases;
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedRoom()
    {
        roomsCanvases.CurrentRoomCanvas.Show();
        roomsCanvases.CreateOrJoinCanvas.Hide();
        content.DestroyChildren();
        roomListings.Clear();
        PhotonNetwork.LeaveLobby();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo roomInfo in roomList)
        {
            if (roomInfo.RemovedFromList)
            {
                int index = roomListings.FindIndex(x => x.RoomInfo.Name == roomInfo.Name);
                if (index != -1)
                {
                    Destroy(roomListings[index].gameObject);
                    roomListings.RemoveAt(index);
                }
            }
            else
            {
                int index = roomListings.FindIndex(x => x.RoomInfo.Name == roomInfo.Name);
                if (index == -1)
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
}