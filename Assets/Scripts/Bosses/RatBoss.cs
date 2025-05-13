using System.Collections;
using UnityEngine;


public enum RatBossState { Idle, Walking, Running, GoopBall, RPunch, LPunch, SpinAttack, SpinKick, Dead}

public class RatBoss : MonoBehaviour
{
    private float Health = 300;
    private float AttackRange = 10;
    private float WalkRange = 30;
    private Animator anim;
    private Transform PlayerPos;
    private Coroutine stateCoroutine;
    private Transform lastRotation;
    private RatBossState curState;


    public float followSpeed;
    public bool isAttacking;



    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        curState = RatBossState.Idle;
        anim.applyRootMotion = true;

        GameObject rotationHolder = new GameObject("RotationHolder");
        lastRotation = rotationHolder.transform;
        lastRotation.rotation = transform.rotation;

        StartCoroutine(Idle());

    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
        {
            Dead();
        }

        PlayerPos = GameObject.FindWithTag("Player").GetComponent<Transform>();

        if (curState == RatBossState.Idle)
        {

            transform.rotation = lastRotation.rotation;
        }

        else if (curState == RatBossState.Walking || curState == RatBossState.Running
                    || curState == RatBossState.SpinKick || curState == RatBossState.GoopBall
                    || curState == RatBossState.SpinAttack) 
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

    private void ChangeState(RatBossState newState)
    {
        if (stateCoroutine != null) StopCoroutine(stateCoroutine);
        curState = newState;

        switch (newState)
        {
            case RatBossState.Idle: stateCoroutine = StartCoroutine(Idle()); break;
            case RatBossState.Walking: stateCoroutine = StartCoroutine(Walk()); break;
            case RatBossState.Running: stateCoroutine = StartCoroutine(Run()); break;
            case RatBossState.GoopBall: stateCoroutine = StartCoroutine(GoopBall()); break;
            case RatBossState.RPunch: stateCoroutine = StartCoroutine(RPunch()); break;
            case RatBossState.LPunch: stateCoroutine = StartCoroutine(LPunch()); break;
            case RatBossState.SpinAttack: stateCoroutine = StartCoroutine(SpinAttack()); break;
            case RatBossState.SpinKick: stateCoroutine = StartCoroutine(SpinKick()); break;
            case RatBossState.Dead: stateCoroutine = StartCoroutine(Dead()); break;
        }
    }

    void ResetAllTriggers()
    {
        anim.ResetTrigger("IsIdle"); anim.ResetTrigger("IsWalking"); anim.ResetTrigger("IsRunning");
        anim.ResetTrigger("IsGoopBall"); anim.ResetTrigger("RPunch"); anim.ResetTrigger("LPunch");
        anim.ResetTrigger("IsSpinning"); anim.ResetTrigger("IsStomp"); anim.ResetTrigger("IsHurricaneKick");

    }

    IEnumerator Idle()
    {
        Debug.Log("Idle");

        followSpeed = 0;
        ResetAllTriggers();

        anim.SetTrigger("IsWalking");
        yield return new WaitForSeconds(2f);
        anim.SetTrigger("RPunch");

        float dist = Vector3.Distance(transform.position, PlayerPos.position);

        if (dist >= AttackRange) { ChangeState(RatBossState.Walking); }
            
    }

    IEnumerator Walk()
    {
        anim.SetTrigger("IsWalking");
        yield return new WaitForSeconds(2f);
        anim.SetTrigger("RPunch");
        yield return null;
    }

    IEnumerator Run()
    {
        yield return null;
    }

    IEnumerator GoopBall()  
    {
        yield return null;
    }

    IEnumerator RPunch()
    {
        yield return null;
    }

    IEnumerator LPunch()
    {
        yield return null;
    }

    IEnumerator SpinAttack()
    {
        yield return null;
    }

    IEnumerator SpinKick()
    {
        yield return null;
    }

    IEnumerator Dead()
    {
        yield return null;
    }

}
