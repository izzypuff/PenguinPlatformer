using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    //access other script
    PlayerControl playerControl;

    //animator
    public Animator otherAnim;
    //other character text
    public GameObject textObject;

    //audio source
    public AudioSource NPCsound;

    //control timer that changes scene
    bool timerStart;
    float timer = 0f;
    float maxTime = 8000f;
    bool changeScene = false;

    private void Start()
    {
        //get components
        playerControl = GetComponent<PlayerControl>();
    }

    private void Update()
    {
        //if timer is triggered
        if (timerStart)
        {
            //begin adding to timer
            timer++;
        }
        //once timer reaches max time
        if(timer == maxTime)
        {
            //change scene
            changeScene = true;
        }
        //if change scene
        if (changeScene)
        {
            //change scenes
            SceneManager.LoadScene("Victory");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if colliding with final platform
        if (collision.gameObject.tag == "Final")
        {
            //player cannot move on last platform
            playerControl.frozen = true;
            //other player is now jumping
            otherAnim.SetBool("Jump", true);
            //shows dialogue on screen
            textObject.SetActive(true);
            //start timer
            timerStart = true;
            //play npc sound
            NPCsound.Play();
            //start audio at 1 sec
            NPCsound.time = 1;
        }
    }
}
