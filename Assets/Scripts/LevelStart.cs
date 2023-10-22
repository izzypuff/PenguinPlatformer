using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelStart : MonoBehaviour
{
    //loads level 1 scene
    public void LoadNewScene()
    {
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "Level1Start")
        {
            SceneManager.LoadScene("Level1");
        }
        if (scene.name == "Level2Start")
        {
            SceneManager.LoadScene("Level2");
        }
        if (scene.name == "EndStart")
        {
            SceneManager.LoadScene("End");
        }
    }
}
