using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Windows;
using static UnityEditor.Rendering.InspectorCurveEditor;

public enum RatMiniBossState { Idle, Walking, Running, PrepareAttack, SpinKick, FirstSmash, NextSmash, FinalSmash, LowHealthFinalSmash, Dead }
public class RatMiniBoss : MonoBehaviour
{
    private RatMiniBossState curState;
    private Animator anim;
    private Transform PlayerPos;
    private Coroutine stateCoroutine;
    private Transform lastRotation;
    private float AttackRange = 15;
    private float WalkRange = 30;

    public float RatBossHealth = 300;
    public float followSpeed;
    public bool isAttacking;




    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        isAttacking = false;
        curState = RatMiniBossState.Idle;
        anim.applyRootMotion = true;

        StartCoroutine(Idle());


    }

    // Update is called once per frame
    void Update()
    {

        PlayerPos = GameObject.FindWithTag("Player").GetComponent<Transform>();

        if (curState == RatMiniBossState.Idle)
        {
            transform.rotation = lastRotation.rotation;
        }

        else if (curState == RatMiniBossState.Walking || curState == RatMiniBossState.Running
                    || curState == RatMiniBossState.PrepareAttack
                    || curState == RatMiniBossState.FirstSmash
                    || curState == RatMiniBossState.NextSmash)
        {
            if (PlayerPos)
            {
                Vector3 dir = PlayerPos.position - transform.position;
                dir.y = 0;
                if (dir != Vector3.zero)
                {
                    Quaternion lookRot = Quaternion.LookRotation(dir);
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, followSpeed * Time.deltaTime);
                    lastRotation.rotation = transform.rotation;

                }
            }
        }

    }

    void ChangeState(RatMiniBossState newState)
    {
        if (stateCoroutine != null) StopCoroutine(stateCoroutine);
        curState = newState;

        switch (newState)
        {
            case RatMiniBossState.Idle: stateCoroutine = StartCoroutine(Idle()); break;
            case RatMiniBossState.Walking: stateCoroutine = StartCoroutine(Walk()); break;
            case RatMiniBossState.Running: stateCoroutine = StartCoroutine(Run()); break;
            case RatMiniBossState.PrepareAttack: stateCoroutine = StartCoroutine(PrepareAttack()); break;
            case RatMiniBossState.FirstSmash: stateCoroutine = StartCoroutine(FirstSmash()); break;
            case RatMiniBossState.NextSmash: stateCoroutine = StartCoroutine(NextSmash()); break;
            case RatMiniBossState.FinalSmash: stateCoroutine = StartCoroutine(FinalSmash()); break;
            case RatMiniBossState.LowHealthFinalSmash: stateCoroutine = StartCoroutine(LowHealthFinalSmash()); break;
                //case RatMiniBossState.Dead: stateCoroutine = StartCoroutine(Dead()); break;
        }
    }

    void ResetAllTriggers()
    {
        anim.ResetTrigger("IsIdle"); anim.ResetTrigger("IsWalking"); anim.ResetTrigger("IsRunning");
        anim.ResetTrigger("IsAttacking"); anim.ResetTrigger("IsSmashing"); anim.ResetTrigger("NextSmash");
        anim.ResetTrigger("Finisher"); anim.ResetTrigger("LowHealthFinisher");
    }



    IEnumerator Idle()
    {
        followSpeed = 0;
        ResetAllTriggers();
        anim.SetTrigger("IsIdle");
        yield return new WaitForSeconds(0.5f);

        float dist = Vector3.Distance(transform.position, PlayerPos.position);

        if (dist <= AttackRange) { ChangeState(RatMiniBossState.PrepareAttack); }
        else if (dist < WalkRange) { ChangeState(RatMiniBossState.Walking); }
        else { ChangeState(RatMiniBossState.Running); }
    }

    IEnumerator Walk()
    {
        followSpeed = 2;

        ResetAllTriggers();
        anim.SetTrigger("IsWalking");

        while (true)
        {
            float dist = Vector3.Distance(transform.position, PlayerPos.position);

            if (dist <= AttackRange)
            {
                ChangeState(RatMiniBossState.PrepareAttack);
                yield break;
            }
            if (dist >= WalkRange)
            {
                ChangeState(RatMiniBossState.Running);
                yield break;
            }
            yield return null;
        }
    }

    IEnumerator Run()
    {
        followSpeed = 3;

        ResetAllTriggers();
        anim.SetTrigger("IsRunning");

        while (true)
        {
            float dist = Vector3.Distance(transform.position, PlayerPos.position);

            if (dist <= AttackRange)
            {
                ChangeState(RatMiniBossState.PrepareAttack);
                yield break;
            }
            if (dist <= WalkRange)
            {
                ChangeState(RatMiniBossState.Walking);
                yield break;
            }
            yield return null;
        }
    }

    IEnumerator PrepareAttack()
    {
        followSpeed = 2f;

        ResetAllTriggers();
        anim.SetTrigger("IsAttacking");
        yield return new WaitForSeconds(1f);

        float dist = Vector3.Distance(transform.position, PlayerPos.position);

        if (dist <= AttackRange)
        {
            ChangeState(RatMiniBossState.FirstSmash);
        }
        else
        {
            ChangeState(RatMiniBossState.Walking);
        }
    }

    IEnumerator FirstSmash()
    {
        followSpeed = 1f;

        ResetAllTriggers();
        anim.SetTrigger("IsSmashing");

        yield return new WaitForSeconds(2f);

        float dist = Vector3.Distance(transform.position, PlayerPos.position);

        if (dist <= AttackRange)
        {
            ChangeState(RatMiniBossState.NextSmash);
        }
        else
        {
            ChangeState(RatMiniBossState.Walking);
        }
    }

    IEnumerator NextSmash()
    {

        ResetAllTriggers();
        anim.SetTrigger("NextSmash");
        yield return new WaitForSeconds(2f);

        float dist = Vector3.Distance(transform.position, PlayerPos.position);

        // If player is still in range, do a finisher move based on health
        if (dist <= AttackRange)
        {
            if (RatBossHealth <= 150)
            {
                ChangeState(RatMiniBossState.LowHealthFinalSmash);
            }
            else
            {
                ChangeState(RatMiniBossState.FinalSmash);
            }
        }
        else
        {
            ChangeState(RatMiniBossState.Walking);
        }
    }

    IEnumerator FinalSmash()
    {

        ResetAllTriggers();
        anim.SetTrigger("Finisher");
        yield return new WaitForSeconds(2f);

        ChangeState(RatMiniBossState.Idle);
    }

    IEnumerator LowHealthFinalSmash()
    {

        ResetAllTriggers();
        anim.SetTrigger("LowHealthFinisher");
        yield return new WaitForSeconds(2f);

        ChangeState(RatMiniBossState.Idle);
    }

    IEnumerator Dead()
    {
        followSpeed = 0f;
        ResetAllTriggers();
        anim.SetTrigger("IsDead");
        // You can set a "Dead" trigger or play a death animation here.
        // anim.SetTrigger("IsDead");
        // Maybe destroy or disable the object after a delay.
        yield break;
    }

    // Call this method when the boss should die:
    public void Die()
    {
        ChangeState(RatMiniBossState.Dead);
    }

}


