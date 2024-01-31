using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class PlayerAudio : MonoBehaviour
{
    public AudioClip[] playerSFX;
    public AudioSource[] audioSources;

    public void PlayAudio(string audioName)
    {
        AudioClip foundClip = Array.Find(playerSFX, clip => clip.name == audioName);

        if(foundClip != null)
        {
            foreach(var audioSource in audioSources)
            {
                if(!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(foundClip);
                    return; //Exits Early Before Continuing Loop
                } else
                {
                    Debug.Log("All Sources Are Playing");
                }
            }
        } else 
        {
            Debug.Log("Audio Clip Not Found");
        }   
    }
}
