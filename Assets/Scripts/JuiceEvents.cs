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
        playerControl = GetComponent<PlayerControl>();
        juiceAnim = GetComponent<Animator>();
    }

    public void Update()
    {
        if (fallDead)
        {
            FallDieJuice();
        }
        if (enemyDead)
        {
            EnemyDieJuice();
        }
    }

    public void JumpJuice(float dir)
    {
        trailRenderer.emitting = true;
    }

    public void LandJuice(float dir)
    {
        trailRenderer.emitting = false;
        landDust.transform.localScale = new Vector3(Mathf.Sign(dir), 1f, 1f);
        changeDirDust.Play();
    }

    public void HMoveJuice(float dir)
    {
        if (lastDir != Mathf.Sign(dir))
        {
            landDust.transform.localScale = new Vector3(Mathf.Sign(dir), 1f, 1f);
            changeDirDust.Play();
        }
        lastDir = Mathf.Sign(dir);
    }

    public void FallDieJuiceStart(CinemachineImpulseSource source)
    {
        StartFallDeathAnim();
        DoCamShake(source);
        fallDead = true;
    }

    public void StartFallDeathAnim()
    {
        juiceAnim.SetBool("fallDeath", true);
    }

    public void DoCamShake(CinemachineImpulseSource source)
    {
        source.GenerateImpulseWithForce(camPower);
    }

    void FallDieJuice()
    {
        fallDeathCounter -= Time.deltaTime;
        if (fallDeathCounter <= 0)
        {
            EndFallDieAnim();
            playerControl.ResetPos();
            fallDeathCounter = fallTimeToWait;
            fallDead = false;
        }
    }

    public void EndFallDieAnim()
    {
        juiceAnim.SetBool("fallDeath", false);
    }

    public void EnemyDieJuiceStart(CinemachineImpulseSource source)
    {
        StartEnemyDeathAnim();
        DoCamShake(source);
        enemyDead = true;
    }

    public void StartEnemyDeathAnim()
    {
        juiceAnim.SetBool("enemyDeath", true);
    }

    void EnemyDieJuice()
    {
        enemyDeathCounter -= Time.deltaTime;
        if(enemyDeathCounter <= 0)
        {
            EndEnemyDieAnim();
            playerControl.ResetPos();
            enemyDeathCounter = enemyTimeToWait;
            enemyDead = false;
        }
    }

    public void EndEnemyDieAnim()
    {
        juiceAnim.SetBool("enemyDeath", false);
    }
}
