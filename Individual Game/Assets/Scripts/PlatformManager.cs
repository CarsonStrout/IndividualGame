using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    // Array setup for platforms
    GameObject[] platforms;
    GameObject currentPlatform;
    int index = 0;

    // GameObject for coin appearing above platform
    public GameObject coin;

    private void Start()
    {
        NewPlatform();
    }

    public void NewPlatform()
    {
        platforms = GameObject.FindGameObjectsWithTag("Platform"); // Creates an array of all objects with the tag platform
        currentPlatform = platforms[index]; // registers random platform as the one the player must get to
        if (index < platforms.Length)
        {
            index++;
            coin.transform.position = new Vector2(currentPlatform.transform.position.x, currentPlatform.transform.position.y); // moves to next platform
        }
    }
}
