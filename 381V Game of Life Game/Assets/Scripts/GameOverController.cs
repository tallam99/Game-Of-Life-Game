using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    public static GameOverController instance;
    public Text gameOverText;
    public string message;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        gameOverText.gameObject.SetActive(false);
    }

    public void DisplayMessage()
    {
        gameOverText.gameObject.SetActive(true);
        gameOverText.text = message;
    }
}
