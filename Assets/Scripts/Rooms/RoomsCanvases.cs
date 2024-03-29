using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RoomsCanvases : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private CreateOrJoinCanvas createOrJoinCanvas;
    public CreateOrJoinCanvas CreateOrJoinCanvas { get { return createOrJoinCanvas; } }

    [SerializeField]
    private CurrentRoomCanvas currentRoomCanvas;
    public CurrentRoomCanvas CurrentRoomCanvas { get { return currentRoomCanvas; } }

    public override void OnEnable()
    {
        base.OnEnable();
        Cursor.visible = true;
    }

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
