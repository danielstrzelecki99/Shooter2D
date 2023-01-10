using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerListingMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Transform content;
    [SerializeField] PlayerListing playerListing;

    private List<PlayerListing> playerListings = new List<PlayerListing>();
    private RoomsCanvases roomsCanvases;

    public GameObject startGameButton;
    public GameObject readyButton;
    public TextMeshProUGUI readyButtonText;

    private bool ready = false;
    private bool host = false;

    public override void OnEnable()
    {
        base.OnEnable();
        ShowOrHideStartGameButton();
        SetReady(false);
        CheckIfPlayerIsHost();
        if (host)
            base.photonView.RPC("RPC_ChangeHostPlayer", RpcTarget.AllViaServer, PhotonNetwork.LocalPlayer, host);
    }

    private void Update()
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
            if (host)
                base.photonView.RPC("RPC_ChangeHostPlayer", RpcTarget.All, PhotonNetwork.LocalPlayer, host); // method which send information about host player to another players when new player join
            PlayerListing listing = Instantiate(playerListing, content);
            if (null != listing)
            {
                listing.SetPlayerInfo(player);
                playerListings.Add(listing);
            }
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (PhotonNetwork.CurrentRoom.IsVisible) // check if players are in lobby
        {
            ShowOrHideStartGameButton(); // check if host player left
            int index = playerListings.FindIndex(x => x.Player == otherPlayer);
            if (index != -1)
            {
                Destroy(playerListings[index].gameObject);
                playerListings.RemoveAt(index);
            }
        }
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        if (PhotonNetwork.CurrentRoom.IsVisible) // check if players are in lobby
        {
            CheckIfPlayerIsHost();

            base.photonView.RPC("RPC_ChangeHostPlayer", RpcTarget.AllBuffered, newMasterClient, host); // method which send information about host player to another players
        }
    }

    public void OnClick_StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount <= 1) return;
            for(int i = 0; i < playerListings.Count; i++) // check if all players are ready
            {
                if (playerListings[i].Player != PhotonNetwork.LocalPlayer) // check if player on list is not host player
                {
                    if (!playerListings[i].Ready)
                        return;
                }
            }

            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
            PhotonNetwork.LoadLevel(PhotonNetwork.CurrentRoom.CustomProperties["Map"].ToString());
        }
    }

    public void ShowOrHideStartGameButton()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            startGameButton.SetActive(true);
            readyButton.SetActive(false);
        }
        else
        {
            startGameButton.SetActive(false);
            readyButton.SetActive(true);
        }
    }

    private void SetReady(bool ready)
    {
        this.ready = ready;
        if (ready)
        {
            readyButtonText.text = "READY";
            readyButtonText.color = Color.green;
        }
        else
        {
            readyButtonText.text = "NOT READY";
            readyButtonText.color = Color.red;
        }
    }

    private void CheckIfPlayerIsHost()
    {
        if (PhotonNetwork.IsMasterClient)
            host = true;
        else
            host = false;
    }

    public void OnClick_Ready()
    {
        SetReady(!ready);
        base.photonView.RPC("RPC_ChangeReadyState", RpcTarget.All, PhotonNetwork.LocalPlayer, ready); // method which send information about ready players to another players
    }

    [PunRPC]
    private void RPC_ChangeReadyState(Player player, bool ready)
    {
        int index = playerListings.FindIndex(x => x.Player == player);
        if (index != -1)
        {
            playerListings[index].Ready = ready;
        }
    }

    [PunRPC]
    private void RPC_ChangeHostPlayer(Player player, bool host)
    {
        int index = playerListings.FindIndex(x => x.Player == player);
        if (index != -1)
        {
            playerListings[index].Host = host;
        }
    }
}
