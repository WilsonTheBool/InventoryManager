using System;
using System.Collections.Generic;
using UnityEngine;
public class SoundManager : MonoBehaviour
{
    public List<AudioClip> soundsList;
    public List<AudioClip> musicList;

    [SerializeField]
    private AudioSource soundPlayer;

    public void PlaySoundByName(string name)
    {
        AudioClip clip = soundsList.Find((AudioClip c) => c.name == name);

        if(clip != null)
        {
            soundPlayer.PlayOneShot(clip);
        }
        else
        {
            Debug.LogError("AudioClip not found: " + name);
        }
    }
}
