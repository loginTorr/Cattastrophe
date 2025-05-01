using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public enum RatMiniBossState { Idle, Walking, Running, PrepareAttack, SpinKick, SmashAttack, Dead }
public class RatMiniBoss : MonoBehaviour
{
    private RatMiniBossState curRatBossState;
    private Rigidbody rb;
    private Animator RatMiniBossAnimator;
    private bool SwitchingStates;
    private Vector3 LastPos;

    private Transform PlayerPos;

    public float RatBossHealth = 300;
    public float followSpeed = 100;



    // Start is called before the first frame update
    void Start()
    {
        RatMiniBossAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        curRatBossState = RatMiniBossState.Idle;
        RatMiniBossAnimator.applyRootMotion = true;

        LastPos = transform.position;

        if (PlayerPos == null)
        {
            if (GameObject.FindWithTag("Player") != null)
            {
                PlayerPos = GameObject.FindWithTag("Player").GetComponent<Transform>();
            }
        }
        StartCoroutine(Idle());


    }

    // Update is called once per frame
    void Update()
    {
        var step = followSpeed * Time.deltaTime;
        Quaternion rot = transform.rotation;
        
        switch (curRatBossState)
        {
            case RatMiniBossState.Idle:
                break;

            case RatMiniBossState.Walking:
                transform.LookAt(PlayerPos.transform);
                transform.rotation = Quaternion.Lerp(rot, transform.rotation, step);
                break;

             case RatMiniBossState.Running:
                 transform.LookAt(PlayerPos.transform);
                 transform.rotation = Quaternion.Lerp(rot, transform.rotation, step);
                 break;

             case RatMiniBossState.PrepareAttack:
                transform.LookAt(PlayerPos.transform);

                break;

             case RatMiniBossState.SpinKick:
                break;

             case RatMiniBossState.SmashAttack:
                transform.LookAt(PlayerPos.transform);
                transform.rotation = Quaternion.Lerp(rot, transform.rotation, step);
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
        StartCoroutine(Walk());
        curRatBossState = RatMiniBossState.Walking;

    }

    IEnumerator Walk()
    {
        RatMiniBossAnimator.ResetTrigger("IsIdle"); RatMiniBossAnimator.ResetTrigger("IsRunning"); RatMiniBossAnimator.ResetTrigger("IsRunning"); RatMiniBossAnimator.ResetTrigger("IsAttacking"); RatMiniBossAnimator.ResetTrigger("IsSpinKicking"); RatMiniBossAnimator.ResetTrigger("IsSmashing");

        RatMiniBossAnimator.SetTrigger("IsWalking");
        yield return new WaitForSeconds(3f);
        StartCoroutine(Run());
        curRatBossState = RatMiniBossState.Running;
    }

    IEnumerator Run()
    {
        RatMiniBossAnimator.ResetTrigger("IsIdle"); RatMiniBossAnimator.ResetTrigger("IsRunning"); RatMiniBossAnimator.ResetTrigger("IsRunning"); RatMiniBossAnimator.ResetTrigger("IsAttacking"); RatMiniBossAnimator.ResetTrigger("IsSpinKicking"); RatMiniBossAnimator.ResetTrigger("IsSmashing");

        RatMiniBossAnimator.SetTrigger("IsRunning");
        yield return new WaitForSeconds(3f);
        StartCoroutine(PrepareAttack());
        curRatBossState = RatMiniBossState.PrepareAttack;

    }
    IEnumerator PrepareAttack()
    {
        RatMiniBossAnimator.ResetTrigger("IsIdle"); RatMiniBossAnimator.ResetTrigger("IsRunning"); RatMiniBossAnimator.ResetTrigger("IsRunning"); RatMiniBossAnimator.ResetTrigger("IsAttacking"); RatMiniBossAnimator.ResetTrigger("IsSpinKicking"); RatMiniBossAnimator.ResetTrigger("IsSmashing");

        RatMiniBossAnimator.SetTrigger("IsAttacking");
        yield return new WaitForSeconds(1f);
        StartCoroutine(Smash());
        curRatBossState = RatMiniBossState.SmashAttack;

    }

    IEnumerator Smash()
    {
        RatMiniBossAnimator.ResetTrigger("IsIdle"); RatMiniBossAnimator.ResetTrigger("IsRunning"); RatMiniBossAnimator.ResetTrigger("IsRunning"); RatMiniBossAnimator.ResetTrigger("IsAttacking"); RatMiniBossAnimator.ResetTrigger("IsSpinKicking"); RatMiniBossAnimator.ResetTrigger("IsSmashing");

        RatMiniBossAnimator.SetTrigger("IsSmashing");

        yield return new WaitForSeconds(5f);

        StartCoroutine(Idle());
        curRatBossState = RatMiniBossState.Idle;

    }

}
