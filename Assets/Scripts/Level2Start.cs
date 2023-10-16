using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2Start : MonoBehaviour
{
    //loads level 2 scene
    public void LoadNewScene()
    {
        SceneManager.LoadScene("Level2");
    }
}
