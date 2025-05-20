using System.Collections;
using UnityEngine;
using static UnityEditor.Rendering.InspectorCurveEditor;


public enum RatBossState { Idle, Walking, Running, GoopBall, RPunch, LPunch, SpinAttack, SpinKick, Dead}

public class RatBoss : MonoBehaviour
{
    private RatBossState curState;
    private Animator anim;
    private Transform PlayerPos;
    private EnemyHealthBar healthBar;
    private Coroutine stateCoroutine;
    private int count;

    public int RatBossHealth;
    public float followSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {
        RatBossHealth = 500;
        count = 0;

        anim = GetComponent<Animator>();
        anim.applyRootMotion = true;

        StartCoroutine(Idle());

        //healthBar = GetComponentInChildren<EnemyHealthBar>();
        //healthBar.SetMaxHealth(RatBossHealth);
    }

    // Update is called once per frame
    void Update()
    {

        //healthBar.SetHealth(RatBossHealth);

        PlayerPos = GameObject.FindWithTag("Player").GetComponent<Transform>();

        if (PlayerPos)
        {
            Vector3 dir = PlayerPos.position - transform.position;
            dir.y = 0;
            if (dir != Vector3.zero)
            {
                Quaternion lookRot = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, followSpeed * Time.deltaTime);

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
        Debug.Log("enteredIdle");
        ResetAllTriggers();
        anim.SetTrigger("IsIdle");
        yield return new WaitForSeconds(1f);
        if (count == 1)
        {
            ChangeState(RatBossState.GoopBall);
        }

        else if (RatBossHealth > 150)
        {
            ChangeState(RatBossState.Walking);
        }

        else
        {
            if (count == 3)
            {

            }
            ChangeState(RatBossState.Running);
        }
    }

    IEnumerator Walk()
    {
        ResetAllTriggers();

        anim.SetTrigger("IsWalking");
        Debug.Log("Change State Succesful");

        yield return null;

    }

    IEnumerator Run()
    {
        ResetAllTriggers();

        yield return null;
    }

    IEnumerator GoopBall()
    {
        ResetAllTriggers();

        yield return null;
    }

    IEnumerator RPunch()
    {
        ResetAllTriggers();

        yield return null;
    }

    IEnumerator LPunch()
    {
        ResetAllTriggers();

        yield return null;
    }

    IEnumerator SpinAttack()
    {
        ResetAllTriggers();

        yield return null;
    }

    IEnumerator SpinKick()
    {
        ResetAllTriggers();

        yield return null;
    }

    IEnumerator Dead()
    {
        ResetAllTriggers();

        yield return null;
    }

}
