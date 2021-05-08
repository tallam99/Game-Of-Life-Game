using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public AudioSource jumpAudioData;
    public AudioSource switchAudioData;
    public AudioSource musicAudioData;
    public bool gameActive;

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
        if (PlayerPrefs.GetInt("music") == 0)
        {
            musicAudioData.mute = true;
        }
        else
        {
            musicAudioData.mute = false;
            musicAudioData.volume = 1.0f;
            musicAudioData.Play();
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
        gameActive = true;
    }

    public void EndGame()
    {
        TimerController.instance.EndTimer();
        GameOverController.instance.DisplayMessage();
        PlayerMovementController.instance.Die();
        CountdownController.instance.countdownText.gameObject.SetActive(false);
        gameActive = false;
    }
}
