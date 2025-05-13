using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Rendering.InspectorCurveEditor;

public enum BossState { Idle, Walking, Running, GoopBall, RPunch, LPunch, SpinAttack, SpinKick, Dead}
public class RatBoss : MonoBehaviour
{
    private float RatBossHealth = 300;
    private float AttackRange = 15;
    private float WalkRange = 30;
    private Animator anim;
    private Transform PlayerPos;
    private Coroutine stateCoroutine;
    private Transform lastRotation;
    private BossState curState;


    public float followSpeed;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (RatBossHealth <= 0)
        {
            //Dead();
        }

        PlayerPos = GameObject.FindWithTag("Player").GetComponent<Transform>();

        if (curState == BossState.Idle)
        {
            transform.rotation = lastRotation.rotation;
        }

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

    void ChangeState(BossState newState)
    {
        if (stateCoroutine != null) StopCoroutine(stateCoroutine);
        curState = newState;

        switch (newState)
        {
            case BossState.Idle: stateCoroutine = StartCoroutine(Idle()); break;
            case BossState.Walking: stateCoroutine = StartCoroutine(Walk()); break;
            case BossState.Running: stateCoroutine = StartCoroutine(Run()); break;
            case BossState.GoopBall: stateCoroutine = StartCoroutine(GoopBall()); break;
            case BossState.RPunch: stateCoroutine = StartCoroutine(RPunch()); break;
            case BossState.LPunch: stateCoroutine = StartCoroutine(LPunch()); break;
            case BossState.SpinAttack: stateCoroutine = StartCoroutine(SpinAttack()); break;
            case BossState.SpinKick: stateCoroutine = StartCoroutine(SpinKick()); break;
            case BossState.Dead: stateCoroutine = StartCoroutine(Dead()); break;
        }
    }

    void ResetAllTriggers()
    {

    }

    IEnumerator Idle()
    {
        yield return null;
    }

    IEnumerator Walk()
    {
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
