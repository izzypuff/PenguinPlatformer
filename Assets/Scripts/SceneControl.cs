using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    // accessed by button
    public void LoadGame (string sceneToLoad)
    {
        //use scene manager to load game scene
        SceneManager.LoadScene(sceneToLoad);
    }
}
