using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    private bool ended = false;

    public static GameOverController instance;
    public Text gameOverText;
    public Text instructions;
    public Text score;
    public PlayerMovementController player;
    public TimerController timer;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        gameOverText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(player.transform.position.y < -25 && !ended)
        {
            GameController.instance.EndGame();
            ended = true;
        }

        score.text = ((int)(player.GetDistance() * timer.GetCurrentTime())).ToString();

        if (ended)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else if (Input.GetKeyDown(KeyCode.M))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            }
        }
    }

    public bool isEnded()
    {
        return ended;
    }

    public void DisplayMessage()
    {
        gameOverText.gameObject.SetActive(true);
        gameOverText.text = "SCORE: " + ((int) (player.GetDistance() * timer.GetCurrentTime())).ToString();
        instructions.text = "Press R to restart, or M to return to the main menu";
    }
}
