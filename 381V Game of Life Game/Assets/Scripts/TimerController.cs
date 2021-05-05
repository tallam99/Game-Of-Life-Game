using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// used tutorial https://youtu.be/qc7J0iei3BU
public class TimerController : MonoBehaviour
{
    public static TimerController instance;

    public Text timerText;
    private bool timerActive;
    private float currentTime;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        timerText.text = "Time: 00:00.00";
        timerActive = false;
    }

    public void StartTimer()
    {
        timerActive = true;
        currentTime = 0f;
        StartCoroutine(UpdateTimer());
    }

    public void EndTimer()
    {
        timerActive = false;
    }

    private IEnumerator UpdateTimer()
    {
        while (timerActive)
        {
            currentTime += Time.deltaTime;
            TimeSpan timePlaying = TimeSpan.FromSeconds(currentTime);
            string timeStr = "Time: " + timePlaying.ToString("mm':'ss'.'ff");
            timerText.text = timeStr;

            yield return null;
        }
    }
}
