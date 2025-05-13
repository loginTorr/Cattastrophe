using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerMovementState
{
    Idle,
    Moving,
    Dashing,
    Dance,
    FirstAttack,
    SecondAttack,
    Finisher,
    RangedAttack
}

public class PlayerMovement : MonoBehaviour
{
    [Header("Audio")]
    public AudioClip[] footstepSounds;
    public AudioClip dashSound;
    public AudioClip jumpSound;
    public AudioClip landSound;
    public AudioClip[] attackSounds;


    public float CurSpeed = 10f;
    public float MaxSpeed = 10f;
    public int AttackDamage = 5;
    public float CurHealth = 100f;
    public float MaxHealth = 100f;

    public bool IsDashing = false;
    public float turnSpeed = 1080f;
    public float footstepInterval = 0.5f;
    public bool isAttacking = false;
    public bool paused;

    private PlayerMovementState CurState;
    private Coroutine stateCoroutine;
    private Rigidbody rb;
    private Vector3 input;
    private Animator anim;
    private SlashAttackTriggers SlashAttackTriggersScript;
    private SoundFXManager soundManager;
    private float lastFootstepTime;

    public bool isGettingKockedBack = false;

    private void Awake()
    {
        soundManager = SoundFXManager.Instance;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        SlashAttackTriggersScript = GetComponentInChildren<SlashAttackTriggers>();
        anim = GetComponentInChildren<Animator>();

        paused = false;
        ChangeState(PlayerMovementState.Idle);
    }

    void Update()
    {
        CheckForFootsteps();
        GatherInput();
        Look();

        SlashAttackTriggersScript.damage = AttackDamage;

        if (!paused && !isGettingKockedBack)
        {
            Move();
        }
    }

    void ChangeState(PlayerMovementState newState)
    {
        if (stateCoroutine != null)
        {
            StopCoroutine(stateCoroutine);
        }

        CurState = newState;

        switch (newState)
        {
            case PlayerMovementState.Idle:
                stateCoroutine = StartCoroutine(Idle());
                break;
            case PlayerMovementState.FirstAttack:
                stateCoroutine = StartCoroutine(FirstAttack());
                break;
            case PlayerMovementState.SecondAttack:
                stateCoroutine = StartCoroutine(SecondAttack());
                break;
            case PlayerMovementState.Finisher:
                stateCoroutine = StartCoroutine(Finisher());
                break;
            default:
                stateCoroutine = StartCoroutine(Idle());
                break;
        }
    }

    void GatherInput()
    {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }

    void Look()
    {
        
        if (input != Vector3.zero)
        {
            Vector3 relative = (transform.position + input) - transform.position; // adjust if you use ToIso
            Quaternion rot = Quaternion.LookRotation(relative, Vector3.up);
            transform.rotation = rot;
        }
    }

    void Move()
    {
        if (IsDashing) return;
        if (isAttacking) { CurSpeed = (MaxSpeed / 2); anim.ResetTrigger("IsRunning"); }
        if (input.magnitude <= 0.1) { anim.ResetTrigger("IsRunning"); anim.SetTrigger("IsIdle"); }
        else { CurSpeed = MaxSpeed; anim.ResetTrigger("IsIdle"); anim.SetTrigger("IsRunning"); }

        Vector3 moveDir = transform.forward * input.magnitude * CurSpeed;

        Vector3 newVelocity = new Vector3(moveDir.x, rb.velocity.y, moveDir.z);

        Vector3 horizontal = new Vector3(newVelocity.x, 0, newVelocity.z);

        if (horizontal.magnitude > MaxSpeed)
        {
            horizontal = horizontal.normalized * MaxSpeed;
        }

        rb.velocity = new Vector3(horizontal.x, newVelocity.y, horizontal.z);
    }

    private void FixedUpdate()
    {
        
    }
    void ResetTriggers()
    {
        anim.ResetTrigger("IsIdle");
        anim.ResetTrigger("IsRunning");
        anim.ResetTrigger("FirstHook");
        anim.ResetTrigger("SecondHook");
        anim.ResetTrigger("Finisher");
    }

    public void RecieveKockback(float magnitude, Vector3 direction)
    {
        Vector3 KnockbackVector = direction * magnitude;
        Vector3 newVelocity = new Vector3(KnockbackVector.x, rb.velocity.y, KnockbackVector.z);
        rb.velocity = newVelocity;
    }

    // IDLE STATE
    IEnumerator Idle()
    {
        isAttacking = false;
        ResetTriggers();

        while (true)
        {
            if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
            {
                ChangeState(PlayerMovementState.FirstAttack);
                yield break;
            }

            yield return null;
        }
    }

    // FIRST ATTACK
    IEnumerator FirstAttack()
    {
        isAttacking = true;
        ResetTriggers();
        anim.SetTrigger("FirstHook");
        if (soundManager != null)
        {
            PlayAttackSound(0);
        }

        // Wait for animation to start playing properly (animation entry time)
        yield return new WaitForSecondsRealtime(0.2f);

        Debug.Log("First attack animation playing, now listening for second attack input");

        // Now start listening for input during a window to chain to second attack
        float timer = 1.0f;
        bool secondAttackRequested = false;

        while (timer > 0f)
        {
            if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
            {
                Debug.Log("Second attack requested during chain window");
                secondAttackRequested = true;
                break;
            }

            timer -= Time.deltaTime;
            yield return null;
        }

        // Make sure we finish the current attack animation
        yield return new WaitForSeconds(0.10f);
        isAttacking = false;


        if (secondAttackRequested)
        {
            Debug.Log("Transitioning to second attack");
            ChangeState(PlayerMovementState.SecondAttack);
        }
        else
        {
            Debug.Log("No follow-up requested, returning to idle");
            isAttacking = false;
            ChangeState(PlayerMovementState.Idle);
        }
    }

    // SECOND ATTACK
    IEnumerator SecondAttack()
    {
        isAttacking = true;
        ResetTriggers();
        anim.SetTrigger("SecondHook");
        if (soundManager != null)
        {
            PlayAttackSound(1);
        }

        // Wait for animation to start playing properly
        yield return new WaitForSecondsRealtime(0.2f);
        Debug.Log("Second attack animation playing, now listening for finisher input");

        // Now start listening for input during a window to chain to finisher
        float timer = 1.0f;
        bool finisherRequested = false;

        while (timer > 0f)
        {
            if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
            {
                Debug.Log("Finisher requested during chain window");
                finisherRequested = true;
                break;
            }

            timer -= Time.deltaTime;
            yield return null;
        }

        // Make sure we finish the current attack animation
        yield return new WaitForSecondsRealtime(0.10f);
        isAttacking = false;


        if (finisherRequested)
        {
            Debug.Log("Transitioning to finisher");
            ChangeState(PlayerMovementState.Finisher);
        }
        else
        {
            Debug.Log("No follow-up requested, returning to idle");
            isAttacking = false;
            ChangeState(PlayerMovementState.Idle);
        }
    }

    // FINISHER
    IEnumerator Finisher()
    {
        isAttacking = true;
        ResetTriggers();
        anim.SetTrigger("Finisher");
        if (soundManager != null)
        {
            PlayAttackSound(2);
        }

        // Wait for the animation to finish playing
        yield return new WaitForSeconds(1f);

        Debug.Log("Finisher complete, returning to idle");
        isAttacking = false;
        anim.SetTrigger("IsIdle");
        ChangeState(PlayerMovementState.Idle);
    }

    void CheckForFootsteps()
    {
        if (input.magnitude > 0.1f && !IsDashing && !isAttacking && !isGettingKockedBack && !paused)
        {
            if (Time.time - lastFootstepTime > footstepInterval)
            {
                PlayFootstepSound();
                lastFootstepTime = Time.time;
            }
        }
    }

    void PlayFootstepSound()
    {
        if (footstepSounds.Length > 0 && soundManager != null)
        {
            int prevIndex = 0;
            int index = Random.Range(0, footstepSounds.Length);

            while (index == prevIndex)
            {
                index = Random.Range(0, footstepSounds.Length);
            }
            soundManager.PlaySound(
                footstepSounds[index],
                transform.position,
                SoundFXManager.SoundCategory.SFX,
                0.35f, // volume
                1.0f, // pitch
                0.1f, // pitch randomness
                0.1f  // volume randomness
            );
            prevIndex = index;
        }
    }

    public void PlayAttackSound(int soundIndex)
    {
        if (attackSounds != null && soundIndex < attackSounds.Length && soundManager != null)
        {
            soundManager.PlaySound(
                attackSounds[soundIndex],
                transform.position,
                SoundFXManager.SoundCategory.SFX,
                0.3f, // volume
                1.0f, // pitch
                0.1f, // pitch randomness
                0.1f  // volume randomness
            );
        }
    }
}