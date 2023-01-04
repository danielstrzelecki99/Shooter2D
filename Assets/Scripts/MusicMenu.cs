using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicMenu : MonoBehaviour
{
    public AudioSource src;
    public AudioClip music;
    private void Awake() {
        src.clip = music;
        src.Play();
    }
}
