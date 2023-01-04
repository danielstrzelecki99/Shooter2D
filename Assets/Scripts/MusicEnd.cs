using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicEnd : MonoBehaviour
{
    public AudioSource src;
    public AudioClip musicSound;
    private void Awake() {
        src.clip = musicSound;
        src.Play();
    }
}
