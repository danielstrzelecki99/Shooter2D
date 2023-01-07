using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider generalVolumeSlider;
    [SerializeField] Slider menuMusicSlider;
    public AudioSource menuMusicSrc;

    void Start()
    {
        if(!PlayerPrefs.HasKey("generalVolume"))
        {
            PlayerPrefs.SetFloat("generalVolume", 1);
            Load();
        } else {
            Load();
        }

        if(!PlayerPrefs.HasKey("menuMusicVolume"))
        {
            PlayerPrefs.SetFloat("menuMusicVolume", 1);
            Load();
        } else {
            Load();
        }    
    }

    public void ChangeValue(){
        AudioListener.volume = generalVolumeSlider.value;
        menuMusicSrc.volume = menuMusicSlider.value;
        Save();
    }

    private void Load(){
        generalVolumeSlider.value = PlayerPrefs.GetFloat("generalVolume");
        menuMusicSlider.value = PlayerPrefs.GetFloat("menuMusicVolume");
    }

    private void Save(){
        PlayerPrefs.SetFloat("generalVolume", generalVolumeSlider.value);
        PlayerPrefs.SetFloat("menuMusicVolume", menuMusicSlider.value);
    }
}
