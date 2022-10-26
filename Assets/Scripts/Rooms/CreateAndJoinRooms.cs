using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using TMPro;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public InputField createInput;
    public Dropdown maxPlayers;
    public Button map1;
    public Button map2;
    public Button map3;
    public GameObject createGameMenu;
    public TextMeshProUGUI errorText;
    private RoomsCanvases roomsCanvases;
    private string selectedMap;
    private Color colorActive = new Color(1, 1, 1, 1);
    private ColorBlock colorBlockActive;
    private Color colorInactive = new Color(1, 1, 1, 0.27f);
    private ColorBlock colorBlockInactive;

    public void Awake()
    {
        colorBlockActive = map1.colors;
        colorBlockActive.normalColor = colorActive;
        colorBlockInactive = map1.colors;
        colorBlockInactive.normalColor = colorInactive;
        map1Click();
    }
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
        Hashtable customProps = new Hashtable();
        customProps.Add("Map", selectedMap);
        options.CustomRoomProperties = customProps;
        if (createInput.text == "")
        {
            errorText.text = "Server name cannot be empty !";
        }
        else
        {
            PhotonNetwork.JoinOrCreateRoom(createInput.text, options, TypedLobby.Default);
            createGameMenu.SetActive(false);
        }
    }

    public override void OnCreatedRoom()
    {
        roomsCanvases.CurrentRoomCanvas.Show();
    }

    public void map1Click()
    {
        resetButtons();
        selectedMap = "Game";
        map1.colors = colorBlockActive;
    }

    public void map2Click()
    {
        resetButtons();
        selectedMap = "WinterMap";
        map2.colors = colorBlockActive;
    }

    public void map3Click()
    {
        resetButtons();
        selectedMap = "EveningMap";
        map3.colors = colorBlockActive;
    }

    public void resetButtons() //method which make all buttons darker
    {
        map1.colors = colorBlockInactive;
        map2.colors = colorBlockInactive;
        map3.colors = colorBlockInactive;
    }
} 
