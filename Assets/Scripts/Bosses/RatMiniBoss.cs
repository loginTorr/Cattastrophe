using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public enum RatMiniBossState { Idle, Walking, Running, PrepareAttack, SpinKick, SmashAttack, Dead }
public class RatMiniBoss : MonoBehaviour
{
    private RatMiniBossState curRatBossState;
    private Animator RatMiniBossAnimator;
    private Transform PlayerPos;
    private Vector3 currentDirection; 

    public float RatBossHealth = 300;
    public float followSpeed;




    // Start is called before the first frame update
    void Start()
    {
        RatMiniBossAnimator = GetComponent<Animator>();

        curRatBossState = RatMiniBossState.Idle;
        RatMiniBossAnimator.applyRootMotion = true;

        StartCoroutine(Idle());


    }

    // Update is called once per frame
    void Update()
    {

        PlayerPos = GameObject.FindWithTag("Player").GetComponent<Transform>();

        switch (curRatBossState)
        {
            case RatMiniBossState.Idle:
                currentDirection.y = 0;
                transform.rotation = Quaternion.LookRotation(currentDirection); 
                break;

            case RatMiniBossState.Walking:
            case RatMiniBossState.Running:
            case RatMiniBossState.SmashAttack:
            case RatMiniBossState.PrepareAttack:
                Vector3 directionToPlayer = PlayerPos.position - transform.position;
                directionToPlayer.y = 0;

                Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
                currentDirection = transform.forward;

                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, followSpeed * Time.deltaTime);
                break;



             case RatMiniBossState.SpinKick:
                break;


             case RatMiniBossState.Dead:
                 break;
        }

        
    }

    IEnumerator Idle()
    {
        RatMiniBossAnimator.ResetTrigger("IsIdle"); RatMiniBossAnimator.ResetTrigger("IsWalking"); RatMiniBossAnimator.ResetTrigger("IsRunning"); RatMiniBossAnimator.ResetTrigger("IsAttacking"); RatMiniBossAnimator.ResetTrigger("IsSpinKicking"); RatMiniBossAnimator.ResetTrigger("IsSmashing");

        RatMiniBossAnimator.SetTrigger("IsIdle");

        yield return new WaitForSeconds(2f);
        curRatBossState = RatMiniBossState.Walking;
        StartCoroutine(Walk());

    }

    IEnumerator Walk()
    {
        RatMiniBossAnimator.ResetTrigger("IsIdle"); RatMiniBossAnimator.ResetTrigger("IsRunning"); RatMiniBossAnimator.ResetTrigger("IsRunning"); RatMiniBossAnimator.ResetTrigger("IsAttacking"); RatMiniBossAnimator.ResetTrigger("IsSpinKicking"); RatMiniBossAnimator.ResetTrigger("IsSmashing");

        followSpeed = 1;
        RatMiniBossAnimator.SetTrigger("IsWalking");

        yield return new WaitForSeconds(3f);
        curRatBossState = RatMiniBossState.Running;
        StartCoroutine(Run());

    }

    IEnumerator Run()
    {
        RatMiniBossAnimator.ResetTrigger("IsIdle"); RatMiniBossAnimator.ResetTrigger("IsRunning"); RatMiniBossAnimator.ResetTrigger("IsRunning"); RatMiniBossAnimator.ResetTrigger("IsAttacking"); RatMiniBossAnimator.ResetTrigger("IsSpinKicking"); RatMiniBossAnimator.ResetTrigger("IsSmashing");

        followSpeed = 2;
        RatMiniBossAnimator.SetTrigger("IsRunning");

        yield return new WaitForSeconds(3f);
        curRatBossState = RatMiniBossState.PrepareAttack;
        StartCoroutine(PrepareAttack());


    }
    IEnumerator PrepareAttack()
    {
        RatMiniBossAnimator.ResetTrigger("IsIdle"); RatMiniBossAnimator.ResetTrigger("IsRunning"); RatMiniBossAnimator.ResetTrigger("IsRunning"); RatMiniBossAnimator.ResetTrigger("IsAttacking"); RatMiniBossAnimator.ResetTrigger("IsSpinKicking"); RatMiniBossAnimator.ResetTrigger("IsSmashing");

        RatMiniBossAnimator.SetTrigger("IsAttacking");

        yield return new WaitForSeconds(1f);
        curRatBossState = RatMiniBossState.SmashAttack;
        StartCoroutine(Smash());


    }

    IEnumerator Smash()
    {
        RatMiniBossAnimator.ResetTrigger("IsIdle"); RatMiniBossAnimator.ResetTrigger("IsRunning"); RatMiniBossAnimator.ResetTrigger("IsRunning"); RatMiniBossAnimator.ResetTrigger("IsAttacking"); RatMiniBossAnimator.ResetTrigger("IsSpinKicking"); RatMiniBossAnimator.ResetTrigger("IsSmashing");

        followSpeed = 3;
        RatMiniBossAnimator.SetTrigger("IsSmashing");

        yield return new WaitForSeconds(5f);
        curRatBossState = RatMiniBossState.Idle;
        StartCoroutine(Idle());


    }

}
