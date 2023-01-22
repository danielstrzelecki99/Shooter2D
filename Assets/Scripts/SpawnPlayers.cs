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
        {44, 19},
        {100, 22},
        {128, 24},
        {78, 40},
        {52, 47},
        {83, 47},
        {80, 58},
        {121, 57},
        {122, 46},
        {61, 29},
        {97, 26},
    };

    public float[,] listOfSpawnsEveningMap =
    {
        {39, 20},
        {70, 24},
        {98, 19},
        {98, 1},
        {117, -29},
        {107, -43},
        {36, -43},
        {23, -28},
        {60, -20},
        {68, 24},
        {52, 10}
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
