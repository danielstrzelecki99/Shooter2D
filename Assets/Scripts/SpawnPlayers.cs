using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;

    public float minX;
    public int maxX;
    public float minY;
    public int maxY;

    public CinemachineVirtualCamera myCinemachine;

    private void Start()
    {
        Vector3 randomPosition = new Vector3(28, 4);
        var player = PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);

        myCinemachine.Follow = player.transform;
    }

}
