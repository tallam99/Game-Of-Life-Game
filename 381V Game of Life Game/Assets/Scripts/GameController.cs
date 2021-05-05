using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

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
