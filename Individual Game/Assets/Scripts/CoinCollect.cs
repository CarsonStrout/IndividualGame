using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollect : MonoBehaviour
{
    public PlatformManager platformManagerScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ScoreCounter.coinAmount < 10)
        {
            ScoreCounter.coinAmount += 1; // increases number of coins collected
            platformManagerScript.NewPlatform(); // calls method to move collectible to next spot
        }
    }
}
