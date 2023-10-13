using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerCollide : MonoBehaviour
{
    JuiceEvents juiceEvents;
    PlayerControl playerControl;
    SceneControl sceneControl;

    private string Victory = "Victory";
    private string Level2 = "Level2";

    private void Start()
    {
        juiceEvents = GetComponent<JuiceEvents>();
        playerControl = GetComponent<PlayerControl>();
        sceneControl = GetComponent<SceneControl>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Enemy" && !PlayerControl.dead)
        {
            PlayerControl.dead = true;
            playerControl.StopPhysics();
            juiceEvents.EnemyDieJuiceStart(collision.gameObject.GetComponent<CinemachineImpulseSource>());
            
        }
        if(collision.gameObject.name == "Goal")
        {
            sceneControl.LoadGame(Victory);
        }

        if(collision.gameObject.name == "Teleport")
        {
            sceneControl.LoadGame(Level2);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Death")
        {
            playerControl.StartReset(collision.gameObject.GetComponent<CinemachineImpulseSource>());
        }
    }
}
