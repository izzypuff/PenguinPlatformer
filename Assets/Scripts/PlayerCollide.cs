using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class PlayerCollide : MonoBehaviour
{
    JuiceEvents juiceEvents;
    PlayerControl playerControl;
    public AudioSource soundPlayer;

    private string Victory = "Victory";
    private string Level2 = "Level2";

    private void Start()
    {
        juiceEvents = GetComponent<JuiceEvents>();
        playerControl = GetComponent<PlayerControl>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy" && !PlayerControl.dead)
        {
            soundPlayer.Play();
            PlayerControl.dead = true;
            playerControl.StopPhysics();
            juiceEvents.EnemyDieJuiceStart(collision.gameObject.GetComponent<CinemachineImpulseSource>());
            
        }

        if(collision.gameObject.name == "Teleport")
        {
            SceneManager.LoadScene(Level2);
        }

        if (collision.gameObject.name == "Goal")
        {
            SceneManager.LoadScene(Victory);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Death")
        {
            soundPlayer.Play();
            playerControl.StartReset(collision.gameObject.GetComponent<CinemachineImpulseSource>());
        }
    }
}
