using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public AudioSource jumpAudioData;
    public AudioSource switchAudioData;

    private void Start()
    {
        if(PlayerPrefs.GetInt("sound") == 0)
        {
            jumpAudioData.mute = true;
            switchAudioData.mute = true;
        }
        else
        {
            jumpAudioData.mute = false;
            switchAudioData.mute = false;
            jumpAudioData.volume = 1.0f;
            switchAudioData.volume = 1.0f;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    public void BeginGame()
    {
        TimerController.instance.StartTimer();
        GridController.instance.StartGrid();
    }

    public void EndGame()
    {
        TimerController.instance.EndTimer();
        GameOverController.instance.DisplayMessage();
        PlayerMovementController.instance.Die();
    }
}
