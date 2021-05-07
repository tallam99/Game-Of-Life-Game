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
}
