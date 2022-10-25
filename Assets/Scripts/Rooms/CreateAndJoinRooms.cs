using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public InputField createInput;
    public Dropdown maxPlayers;
    private RoomsCanvases roomsCanvases;

    public void FirstInitialize(RoomsCanvases canvases)
    {
        roomsCanvases = canvases;
    }

    public void CreateRoom()
    {
        if (!PhotonNetwork.IsConnected) // sprawdzenie czy jest po³¹czenie z serwerem
        {
            return;
        }
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = byte.Parse(maxPlayers.options[maxPlayers.value].text);
        options.IsVisible = true;
        PhotonNetwork.JoinOrCreateRoom(createInput.text, options, TypedLobby.Default);
    }

    public override void OnCreatedRoom()
    {
        roomsCanvases.CurrentRoomCanvas.Show();
    }

    //public void JoinRoom()
    //{
       // PhotonNetwork.JoinRoom(joinInput.text);
//}

    /*public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }*/
} 
