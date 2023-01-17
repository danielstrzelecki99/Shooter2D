using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quit : MonoBehaviour
{
    public Button quit;
    private void Start() {
        quit.onClick.AddListener(() => { QuitGame();});
    }
    public void QuitGame ()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
