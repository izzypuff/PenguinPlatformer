using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1Start : MonoBehaviour
{
    public void LoadNewScene()
    {
        SceneManager.LoadScene("Level1");
    }
}
