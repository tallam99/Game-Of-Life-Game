using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// used tutorial https://youtu.be/ulxXGht5D2U
public class CountdownController : MonoBehaviour
{
    public static CountdownController instance;

    public Text countdownText;
    public int countdownTime;
    public string message;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(CountdownToStart());
    }

    private IEnumerator CountdownToStart()
    {
        while (countdownTime > 0)
        {
            countdownText.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }

        countdownText.text = message;
        // If player dies before countdown finishes, don't restart the game.
        if (!GameOverController.instance.isEnded())
        {
            GameController.instance.BeginGame();
        }

        yield return new WaitForSeconds(1f);
        countdownText.gameObject.SetActive(false);
    }
}
