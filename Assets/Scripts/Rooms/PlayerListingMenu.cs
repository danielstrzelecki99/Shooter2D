using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerListingMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Transform content;
    [SerializeField] PlayerListing playerListing;

    private List<PlayerListing> playerListings = new List<PlayerListing>();
    private RoomsCanvases roomsCanvases;

    private void Awake()
    {
        GetCurrentRoomPlayers();
    }

    public override void OnDisable()
    {
        for (int i = 0; i < playerListings.Count; i++)
            Destroy(playerListings[i].gameObject);
        playerListings.Clear();
    }

    public void FirstInitialize(RoomsCanvases canvases)
    {
        roomsCanvases = canvases;
    }

    private void GetCurrentRoomPlayers()
    {
        if (!PhotonNetwork.IsConnected)
            return;
        if (PhotonNetwork.CurrentRoom == null || PhotonNetwork.CurrentRoom.Players == null)
            return;

        foreach(KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
        {
            AddPlayerListing(playerInfo.Value);
        }
    }

    private void AddPlayerListing(Player player)
    {
        int index = playerListings.FindIndex(x => x.Player == player);
        if (index != -1)
        {
            playerListings[index].SetPlayerInfo(player);
        }
        else
        {
            PlayerListing listing = Instantiate(playerListing, content);
            if (null != listing)
            {
                listing.SetPlayerInfo(player);
                playerListings.Add(listing);
            }
        }
    }

   // public override void OnPlayerEnteredRoom(Player newPlayer)
   // {
     //   AddPlayerListing(newPlayer);
   // }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        int index = playerListings.FindIndex(x => x.Player == otherPlayer);
        if (index != -1)
        {
            Destroy(playerListings[index].gameObject);
            playerListings.RemoveAt(index);
        }
    }

    public void OnClick_StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
            PhotonNetwork.LoadLevel("Game");
        }
    }
}
