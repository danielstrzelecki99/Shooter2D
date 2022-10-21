using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveRoomMenu : MonoBehaviour
{
    private RoomsCanvases roomsCanvases;

    public void OnClickLeaveRoom()
    {
        PhotonNetwork.LeaveRoom(true);
        roomsCanvases.CurrentRoomCanvas.Hide();
        roomsCanvases.CreateOrJoinCanvas.Show();
    }

    public void FirstInitialize(RoomsCanvases canvases)
    {
        roomsCanvases = canvases;
    }
}
