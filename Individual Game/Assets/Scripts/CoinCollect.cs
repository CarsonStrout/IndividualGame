using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinCollect : MonoBehaviour
{
    public PlatformManager platformManagerScript;

    private void Start()
    {
        ScoreCounter.coinAmount = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ScoreCounter.coinAmount < ScoreCounter.coinCheck)
        {
            ScoreCounter.coinAmount += 1; // increases number of coins collected
            platformManagerScript.NewPlatform(); // calls method to move collectible to next spot
        }
    }
}
