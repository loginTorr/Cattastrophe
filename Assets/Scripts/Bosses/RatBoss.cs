using System.Collections;
using UnityEngine;


public enum RatBossState { Idle, Walking, Running, Roaring, GoopBall, RPunch, LPunch, Stab, Melee, SpinKick, Dead}

public class RatBoss : MonoBehaviour
{
    private RatBossState curState;
    private Animator anim;
    private Transform PlayerPos;
    private EnemyHealthBar healthBar;
    private Coroutine stateCoroutine;
    private int count;
    private bool healthy;

    public int RatBossHealth;
    public float followSpeed;

    // Start is called before the first frame update
    void Start()
    {
        RatBossHealth = 400;
        count = 0;
        followSpeed = 2;
        healthy = true;

        anim = GetComponent<Animator>();
        anim.applyRootMotion = true;

        StartCoroutine(Idle());

        //healthBar = GetComponentInChildren<EnemyHealthBar>();
        //healthBar.SetMaxHealth(RatBossHealth);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = transform.position;
        newPosition.y = 0;
        transform.position = newPosition;

        //healthBar.SetHealth(RatBossHealth);

        PlayerPos = GameObject.FindWithTag("Player").GetComponent<Transform>();

        if (curState == RatBossState.Walking || curState == RatBossState.Running || curState == RatBossState.LPunch || curState == RatBossState.SpinKick || curState == RatBossState.Stab)
        {

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
            case RatBossState.Roaring: stateCoroutine = StartCoroutine(Roaring()); break;
            case RatBossState.GoopBall: stateCoroutine = StartCoroutine(GoopBall()); break;
            case RatBossState.RPunch: stateCoroutine = StartCoroutine(RPunch()); break;
            case RatBossState.LPunch: stateCoroutine = StartCoroutine(LPunch()); break;
            case RatBossState.Stab: stateCoroutine = StartCoroutine(Stab()); break;
            case RatBossState.Melee : stateCoroutine = StartCoroutine(Melee()); break;
            case RatBossState.SpinKick: stateCoroutine = StartCoroutine(SpinKick()); break;
            case RatBossState.Dead: stateCoroutine = StartCoroutine(Dead()); break;
        }
    }

    void ResetAllTriggers()
    {
        anim.ResetTrigger("IsIdle"); anim.ResetTrigger("IsWalking"); anim.ResetTrigger("IsRunning"); anim.ResetTrigger("IsRoaring");
        anim.ResetTrigger("IsGoopBall"); anim.ResetTrigger("RPunch"); anim.ResetTrigger("LPunch");
        anim.ResetTrigger("IsStabbing"); anim.ResetTrigger("IsMelee"); anim.ResetTrigger("IsHurricaneKick");

    }

    IEnumerator Idle()
    {
        ResetAllTriggers();
        followSpeed = 2.5f;
        anim.SetTrigger("IsIdle");
        yield return new WaitForSeconds(0.8f);
        if (healthy == true && RatBossHealth <= 200)
        {
            healthy = false;
            ChangeState(RatBossState.Roaring);
            yield break;
        }

        else if (count == 2)
        {
            ChangeState(RatBossState.GoopBall);
        }

        else if (RatBossHealth > 200)
        {
            float dist = Vector3.Distance(transform.position, PlayerPos.position);
            
            if (dist > 5)
            {
                ChangeState(RatBossState.Walking);
            }
            else
            {
                ChangeState(RatBossState.RPunch);
            }
        }

        else
        {
            float dist = Vector3.Distance(transform.position, PlayerPos.position);

            if (count == 3)
            {
                ChangeState(RatBossState.Roaring);
            }
            else if (dist > 5)
            {
                ChangeState(RatBossState.Running);

            }

            else
            {
                ChangeState(RatBossState.RPunch);
            }
        }
    }

    IEnumerator Walk()
    {
        ResetAllTriggers();
        anim.SetTrigger("IsWalking");

        while (true)
        {
            float dist = Vector3.Distance(transform.position, PlayerPos.position);

            if (dist < 5)
            {
                ChangeState(RatBossState.RPunch);
                yield break;
            }
            yield return null;
        }

    }

    IEnumerator Run()
    {
        ResetAllTriggers();
        anim.SetTrigger("IsRunning");

        while (true)
        {
            float dist = Vector3.Distance(transform.position, PlayerPos.position);

            if (dist < 5)
            {
                ChangeState(RatBossState.Stab);
                yield break;
            }
            yield return null;
        }
    }

    IEnumerator Roaring()
    {
        ResetAllTriggers();
        anim.SetTrigger("IsRoaring");
        yield return new WaitForSeconds(1.2f);
        ChangeState(RatBossState.SpinKick);
    }

    IEnumerator GoopBall()
    {
        ResetAllTriggers();
        anim.SetTrigger("IsGoopBall");
        yield return new WaitForSeconds(3f);

        ChangeState(RatBossState.Idle);
    }

    IEnumerator RPunch()
    {
        ResetAllTriggers();
        anim.SetTrigger("RPunch");
        yield return new WaitForSeconds(1f);

        ChangeState(RatBossState.LPunch);
        yield return null;
    }

    IEnumerator LPunch()
    {
        ResetAllTriggers();
        anim.SetTrigger("LPunch");
        yield return new WaitForSeconds(1f);

        ChangeState(RatBossState.Idle);
        yield return null;
    }

    IEnumerator Stab()
    {
        ResetAllTriggers();
        anim.SetTrigger("IsStabbing");
        yield return new WaitForSeconds(1f);

        ChangeState(RatBossState.Melee);
    }

    IEnumerator Melee()
    {
        ResetAllTriggers();
        anim.SetTrigger("IsMelee");
        yield return new WaitForSeconds(1f);

        ChangeState(RatBossState.Idle);
    }

    IEnumerator SpinKick()
    {
        ResetAllTriggers();
        followSpeed = 2f;
        anim.SetTrigger("IsHurricaneKick");
        yield return new WaitForSeconds(3f);

        ChangeState(RatBossState.Idle);
        yield return null;
    }

    IEnumerator Dead()
    {
        ResetAllTriggers();

        yield return null;
    }

}
