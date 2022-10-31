using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoEnd : MonoBehaviour
{
    public VideoPlayer VideoPlayer;
    public string SceneName;
    public LevelLoader levelLoader;

    void Start()
    {
        VideoPlayer.loopPointReached += LoadScene;
    }

    void LoadScene(VideoPlayer vp)
    {
        levelLoader.LoadNextLevel();
    }
}
