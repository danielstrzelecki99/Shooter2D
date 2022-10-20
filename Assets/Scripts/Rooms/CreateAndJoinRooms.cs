using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public InputField createInput;
    public InputField joinInput;
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
        options.MaxPlayers = 10;
        PhotonNetwork.JoinOrCreateRoom(createInput.text, options, TypedLobby.Default);
    }

    public override void OnCreatedRoom()
    {
        roomsCanvases.CurrentRoomCanvas.Show();
        roomsCanvases.CreateOrJoinCanvas.Hide();
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public void StartGameFromRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }

    /*public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }*/
} 
