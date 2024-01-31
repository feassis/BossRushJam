using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    private Slider volumeSlider;
    private TMP_Text volumeNumber;

    private void Awake()
    {
        volumeSlider = transform.Find("VolumeSlider").GetComponent<Slider>();
        volumeNumber = transform.Find("VolumeSlider/VolumeNumber").GetComponent<TMP_Text>();
    }

    private void OnEnable() //When The Settings Menu Is Active, We Adjust The Numbers
    {
        DisplayCurrentVolume();
    }

    public void UpdateVolumeSetting() //Updates Value On Change
    {
        float volume = volumeSlider.value; 
        volumeNumber.text = volume.ToString("0.##");
        PlayerPreferencesManager.SaveVolumeSetting(volume);
    }

    private void DisplayCurrentVolume() //Only Gets Called When Enabled & Returning To Default Settings
    {
        float volume = PlayerPreferencesManager.GetVolumeSetting();
        volumeSlider.value = volume;
        volumeNumber.text = volume.ToString();
    }

    public void DefaultSettingsButton()
    {
        //On Press..
        PlayerPreferencesManager.DefaultSettings(); //Trigger Default Settings
        DisplayCurrentVolume(); //Displays Values
    }
}
