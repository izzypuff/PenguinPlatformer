using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class PlayerCollide : MonoBehaviour
{
    //access other scripts
    JuiceEvents juiceEvents;
    PlayerControl playerControl;

    //audio for death
    public AudioSource soundPlayer;

    //access scenes
    private string EndStart = "EndStart";
    private string Level2Start = "Level2Start";

    //rigid body
    public Rigidbody2D myBody;

    private void Start()
    {
        //get components
        juiceEvents = GetComponent<JuiceEvents>();
        playerControl = GetComponent<PlayerControl>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if colliding with enemy and player is not dead
        if(collision.gameObject.tag == "Enemy" && !PlayerControl.dead)
        {
            //play death sound effect
            soundPlayer.Play();
            //player is now dead
            PlayerControl.dead = true;
            //stop movement
            playerControl.StopPhysics();
            //start enemy death juice effects
            juiceEvents.EnemyDieJuiceStart(collision.gameObject.GetComponent<CinemachineImpulseSource>());
            
        }

        //if colliding with teleport for next level
        if(collision.gameObject.name == "Teleport" && !PlayerControl.dead)
        {
            //load next level scene
            SceneManager.LoadScene(Level2Start);
        }

        //if colliding with end goal
        if (collision.gameObject.name == "Goal" && !PlayerControl.dead)
        {
            //load victory scene
            SceneManager.LoadScene(EndStart);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if colliding with boundary
        if(collision.gameObject.name == "Death")
        {
            //play death sound effect
            soundPlayer.Play();
            //reset character
            playerControl.StartReset(collision.gameObject.GetComponent<CinemachineImpulseSource>());
        }
    }
}
