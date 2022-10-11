using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateOrJoinCanvas : MonoBehaviour
{
    [SerializeField]
    private CreateAndJoinRooms createAndJoinRooms;
    private RoomsCanvases roomsCanvases;

    public void FirstInitialize(RoomsCanvases canvases)
    {
        roomsCanvases = canvases;
        createAndJoinRooms.FirstInitialize(canvases);
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

}
