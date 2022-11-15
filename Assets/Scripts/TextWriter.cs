using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextWriter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    private string textToWrite = "Waiting for statistics...";
    private float timer;
    private float timePerChar = 0.1f;
    private int charIndex = 0;

    void Update()
    {
        if(text != null){
            timer -= Time.deltaTime;
            if(timer <= 0f){
                timer += timePerChar;
                charIndex++;
                text.text = textToWrite.Substring(0, charIndex);

                if(charIndex >= textToWrite.Length){
                    text = null;
                    return;
                }
            }
        }
    }
}
