using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinCollect : MonoBehaviour
{
    public PlatformManager platformManagerScript;
    public PortalController portalControllerScript;

    private void Start()
    {
        ScoreCounter.coinAmount = 0;
        portalControllerScript.portal.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ScoreCounter.coinAmount < ScoreCounter.coinCheck)
        {
            ScoreCounter.coinAmount += 1; // increases number of coins collected
            platformManagerScript.NewPlatform(); // calls method to move collectible to next spot
        }

        if (ScoreCounter.coinAmount == ScoreCounter.coinCheck)
        {
            portalControllerScript.portal.SetActive(true);
        }
    }
}
