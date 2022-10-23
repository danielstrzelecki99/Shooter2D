using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Photon.Pun;
using Cinemachine;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;
    public CinemachineVirtualCamera myCinemachine;

    public float[,] listOfSpawns =
    {
        {28, 0.2f},
        {37, 26},
        {24, 17},
        {5, 6},
        {2, -2},
        {47, 0.2f},
        {65, -2},
        {65, 9},
        {43, 14},
        {58, 20},
        {61.5f, 26}
    };

    private void Start()
    {
        System.Random gen = new System.Random();
        int numberOfSpawnPoint = gen.Next(11);
        Vector3 spawnPosition = new Vector3(listOfSpawns[numberOfSpawnPoint, 0], listOfSpawns[numberOfSpawnPoint, 1]);
        GameObject player;
        if (PhotonNetwork.InRoom)
        {
            player = PhotonNetwork.Instantiate(playerPrefab.name, spawnPosition, Quaternion.identity);
        }
        else
        {
            player = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
        }

        myCinemachine.Follow = player.transform;
    }
}
