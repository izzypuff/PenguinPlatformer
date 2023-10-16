using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class JuiceEvents : MonoBehaviour
{
    //last direction
    float lastDir = 0f;
    //if falling
    float fallDeathCounter;
    bool fallDead = false;
    //about enemy
    float enemyDeathCounter;
    bool enemyDead = false;

    //juice effects
    [Header("Start Jump Juice")]
    public ParticleSystem jumpDust;
    public TrailRenderer trailRenderer;

    [Header("Land Juice")]
    public ParticleSystem landDust;

    [Header("Grounded Movement Juice")]
    public ParticleSystem changeDirDust;

    [Header("Falling Death Juice")]
    public float fallTimeToWait;

    [Header("Enemy Death Juice")]
    public float enemyTimeToWait;
    public float camPower;

    Animator juiceAnim;

    PlayerControl playerControl;

    public void Start()
    {
        //get components
        playerControl = GetComponent<PlayerControl>();
        juiceAnim = GetComponent<Animator>();
    }

    public void Update()
    {
        //if dead to fall
        if (fallDead)
        {
            //use fall juice
            FallDieJuice();
        }
        //if die to enemy
        if (enemyDead)
        {
            //use enemy juice
            EnemyDieJuice();
        }
    }

    //juice when jumping
    public void JumpJuice(float dir)
    {
        //trail enables
        trailRenderer.emitting = true;
    }

    //juice when landing
    public void LandJuice(float dir)
    {
        //enable trail
        trailRenderer.emitting = false;
        //create land dust
        landDust.transform.localScale = new Vector3(Mathf.Sign(dir), 1f, 1f);
        changeDirDust.Play();
    }

    //juice when moving L and R
    public void HMoveJuice(float dir)
    {
        //if last direction is opposite
        if (lastDir != Mathf.Sign(dir))
        {
            //create land dust
            landDust.transform.localScale = new Vector3(Mathf.Sign(dir), 1f, 1f);
            //change up the dir dust
            changeDirDust.Play();
        }
        //sets last direction
        lastDir = Mathf.Sign(dir);
    }

    //starts fall die juice
    public void FallDieJuiceStart(CinemachineImpulseSource source)
    {
        //starts the animation for death
        StartFallDeathAnim();
        //screen shake
        DoCamShake(source);
        //fall death is true
        fallDead = true;
    }

    //starts fall death animation
    public void StartFallDeathAnim()
    {
        //animation bool is true and plays animation
        juiceAnim.SetBool("fallDeath", true);
    }

    //screen shake
    public void DoCamShake(CinemachineImpulseSource source)
    {
        //shakes with allotted cam power float
        source.GenerateImpulseWithForce(camPower);
    }

    //fall death juice
    void FallDieJuice()
    {
        //starts timer (counts down)
        fallDeathCounter -= Time.deltaTime;
        //once timer reaches zero
        if (fallDeathCounter <= 0)
        {
            //stop fall death anim
            EndFallDieAnim();
            //reset players pos
            playerControl.ResetPos();
            //reset timer
            fallDeathCounter = fallTimeToWait;
            //fall death is no longer true bc player got reset
            fallDead = false;
        }
    }

    //end fall die animation
    public void EndFallDieAnim()
    {
        //stop fall death anim
        juiceAnim.SetBool("fallDeath", false);
    }

    //start enemy death juice
    public void EnemyDieJuiceStart(CinemachineImpulseSource source)
    {
        //start the enemy death anim
        StartEnemyDeathAnim();
        //screen shake
        DoCamShake(source);
        //enemy death is true
        enemyDead = true;
    }

    //start enemy death animation
    public void StartEnemyDeathAnim()
    {
        //set animation bool for enemy death true
        juiceAnim.SetBool("enemyDeath", true);
    }

    //enemy death juice
    void EnemyDieJuice()
    {
        //start counting down timer
        enemyDeathCounter -= Time.deltaTime;
        //once time hits zero
        if(enemyDeathCounter <= 0)
        {
            //stop enemy die animation
            EndEnemyDieAnim();
            //reset player pos
            playerControl.ResetPos();
            //reset timer
            enemyDeathCounter = enemyTimeToWait;
            //player is no longer dead bc reset
            enemyDead = false;
        }
    }

    //stop enemy death anim
    public void EndEnemyDieAnim()
    {
        //set enemy death bool to false
        juiceAnim.SetBool("enemyDeath", false);
    }
}
