using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingDelay : MonoBehaviour
{
    
    void Start()
    {
        Invoke("LoadNextScene", 2);
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene("GameEnd");
    }
}
