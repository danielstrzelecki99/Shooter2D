using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateOrJoinCanvas : MonoBehaviour
{
    [SerializeField]
    private CreateAndJoinRooms createAndJoinRooms;
    [SerializeField]
    private RoomListingsMenu roomListingsMenu;
    [SerializeField]
    private GameObject enterPasswordWindow;

    private RoomsCanvases roomsCanvases;
    public RoomInfo room;
    public GameObject badPasswordText;
    public InputField roomPasswordInput;

    public void OnEnable()
    {
        EnterPasswordWindowHide();
    }

    public void FirstInitialize(RoomsCanvases canvases)
    {
        roomsCanvases = canvases;
        createAndJoinRooms.FirstInitialize(canvases);
        roomListingsMenu.FirstInitialize(canvases);
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void EnterPasswordWindowShow()
    {
        enterPasswordWindow.SetActive(true);
    }

    public void EnterPasswordWindowHide()
    {
        enterPasswordWindow.SetActive(false);
    }

    public void OnClickJoin()
    {
        if (roomPasswordInput.text == (string)room.CustomProperties["RoomPassword"])
            PhotonNetwork.JoinRoom(room.Name);
        else
            badPasswordText.SetActive(true);
    }

}
