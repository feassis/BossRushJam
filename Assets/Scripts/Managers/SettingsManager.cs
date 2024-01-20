using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public AudioMixer[] audioMixer; //Array Of Mixers For Editing

    public void SetMusicVolume(float volume) //Function Sets Music Volume
    {
        audioMixer[0].SetFloat("MasterVolume", volume);
    }

    public void SetSoundVolume(float volume) //Function Sets Sound FX Volume
    {
        audioMixer[1].SetFloat("MasterVolume", volume);
    }
}
