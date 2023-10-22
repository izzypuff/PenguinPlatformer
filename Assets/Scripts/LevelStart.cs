using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelStart : MonoBehaviour
{
    public void LoadNewScene()
    {
        //gets active scene
        Scene scene = SceneManager.GetActiveScene();

        //if active scene is level 1 start
        if (scene.name == "Level1Start")
        {
            //load level 1
            SceneManager.LoadScene("Level1");
        }
        //if active scene is load level 2 start
        if (scene.name == "Level2Start")
        {
            //load level 2
            SceneManager.LoadScene("Level2");
        }
        //if active scene is end start
        if (scene.name == "EndStart")
        {
            //load end
            SceneManager.LoadScene("End");
        }
    }
}
