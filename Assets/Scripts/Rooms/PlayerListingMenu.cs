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

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        PlayerListing listing = Instantiate(playerListing, content);
        if (null != listing)
        {
            listing.SetPlayerInfo(newPlayer);
            playerListings.Add(listing);
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        int index = playerListings.FindIndex(x => x.Player == otherPlayer);
        if (index != -1)
        {
            Destroy(playerListings[index].gameObject);
            playerListings.RemoveAt(index);
        }
    }
}
