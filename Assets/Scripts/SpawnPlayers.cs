using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Photon.Pun;
using Cinemachine;
using UnityEngine.SceneManagement;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;
    public CinemachineVirtualCamera myCinemachine;

    public float[,] listOfSpawnsForestMap =
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

    public float[,] listOfSpawnsWinterMap =
    {
        {36, 15},
        {70, 21},
        {36, 15},
        {36, 15},
        {36, 15},
        {36, 15},
        {36, 15},
        {36, 15},
        {36, 15},
        {36, 15},
        {36, 15},
    };

    public float[,] listOfSpawnsEveningMap =
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
        Vector3 spawnPosition;
        String activeSceneName = SceneManager.GetActiveScene().name;
        Debug.Log(activeSceneName);
        if (activeSceneName == "Game")
        {
            spawnPosition = new Vector3(listOfSpawnsForestMap[numberOfSpawnPoint, 0], listOfSpawnsForestMap[numberOfSpawnPoint, 1]);
        }
        else if (activeSceneName == "WinterMap")
        {
            spawnPosition = new Vector3(listOfSpawnsWinterMap[numberOfSpawnPoint, 0], listOfSpawnsWinterMap[numberOfSpawnPoint, 1]);
        }
        else
        {
            spawnPosition = new Vector3(listOfSpawnsEveningMap[numberOfSpawnPoint, 0], listOfSpawnsEveningMap[numberOfSpawnPoint, 1]);
        }
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
