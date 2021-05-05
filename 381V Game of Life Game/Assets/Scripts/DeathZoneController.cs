using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZoneController : MonoBehaviour
{
    private void OnTriggerEnter()
    {
        GameController.instance.EndGame();
    }
}
