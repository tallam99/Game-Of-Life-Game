using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusicDriver : MonoBehaviour
{
    public AudioSource musicAudioData;

    // Start is called before the first frame update
    void Start()
    {
        musicAudioData.Play();
    }

    void Update()
    {
        if(PlayerPrefs.GetInt("music") == 0)
        {
            musicAudioData.mute = true;
        }
        else
        {
            musicAudioData.mute = false;
        }
    }
}
