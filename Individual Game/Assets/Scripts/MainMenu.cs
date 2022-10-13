using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }

    public void LevelOne()
    {
        SceneManager.LoadScene(2);
    }

    public void LevelTwo()
    {
        SceneManager.LoadScene(3);
    }

    public void LevelThree()
    {
        SceneManager.LoadScene(4);
    }

    public void LevelFour()
    {
        SceneManager.LoadScene(5);
    }
}
