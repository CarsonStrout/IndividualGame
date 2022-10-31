using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalController : MonoBehaviour
{
    public TextMeshPro text;
    public GameObject portal;
    private bool touching;
    public LevelLoader levelLoader;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (touching)
            {
                levelLoader.LoadNextLevel();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            text.alpha = 255;
            touching = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            text.alpha = 0;
            touching = false;
        }
    }
}
