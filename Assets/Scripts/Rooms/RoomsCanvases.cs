using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsCanvases : MonoBehaviour
{
    [SerializeField]
    private CreateOrJoinCanvas createOrJoinCanvas;
    public CreateOrJoinCanvas CreateOrJoinCanvas { get { return createOrJoinCanvas; } }

    [SerializeField]
    private CurrentRoomCanvas currentRoomCanvas;
    public CurrentRoomCanvas CurrentRoomCanvas { get { return currentRoomCanvas; } }

    private void Awake()
    {
        FirstInitialize();
    }

    private void FirstInitialize()
    {
        CreateOrJoinCanvas.FirstInitialize(this);
        CurrentRoomCanvas.FirstInitialize(this);
    }
}
