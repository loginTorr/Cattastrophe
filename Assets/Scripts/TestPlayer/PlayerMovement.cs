using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Changable Player Values
    public float MaxSpeed = 8;
    public int AttackDamage = 5;
    public float CurHealth = 100;
    public float MaxHealth = 100;

    public bool IsDashing = false;
    public float turnSpeed = 1080;
    public bool isAttacking = false;
    public bool paused;

    private Rigidbody rb;
    private Vector3 input;
    private Animator PlayerAnim;
    private SlashAttackTriggers SlashAttackTriggersScript;

    public bool isGettingKockedBack = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        SlashAttackTriggersScript = GetComponentInChildren<SlashAttackTriggers>();

        PlayerAnim = GetComponentInChildren<Animator>();

        paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        GatherInput();
        Look();

        SlashAttackTriggersScript.damage = AttackDamage;
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isAttacking )
        {
            ResetTriggers();

            StartCoroutine("AttackSequence");
        }
    }

    private void FixedUpdate()
    {
        if (!paused && !isGettingKockedBack)
        {
            Move();
        }
    }
    void GatherInput() { input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")); }

    void Look()
    {
        PlayerAnim.SetTrigger("IsIdle");

        if (input != Vector3.zero)
        {
            ResetTriggers();
            PlayerAnim.SetTrigger("IsRunning");
            var relative = (transform.position + input.ToIso()) - transform.position;
            var rot = Quaternion.LookRotation(relative, Vector3.up);

            transform.rotation = rot;
        }
    }

    void Move()
    {
        if (IsDashing) { return; }

        Vector3 moveDir = transform.forward * input.magnitude * MaxSpeed;

        Vector3 newVelocity = new Vector3(moveDir.x, rb.velocity.y, moveDir.z);

        Vector3 horizontal = new Vector3(newVelocity.x, 0, newVelocity.z);

        if (horizontal.magnitude > MaxSpeed)
        {
            horizontal = horizontal.normalized * MaxSpeed;
        }

        rb.velocity = new Vector3(horizontal.x, newVelocity.y, horizontal.z);
    }

    public void RecieveKockback(float magnitude, Vector3 direction)
    {
        Vector3 KnockbackVector = direction * magnitude;
        Vector3 newVelocity = new Vector3(KnockbackVector.x, rb.velocity.y, KnockbackVector.z);
        rb.velocity = newVelocity;
    }

    private void ResetTriggers()
    {
        PlayerAnim.ResetTrigger("IsIdle");
        PlayerAnim.ResetTrigger("IsRunning");
        PlayerAnim.ResetTrigger("FirstHook");
        PlayerAnim.ResetTrigger("SecondHook");
        PlayerAnim.ResetTrigger("Finisher");
    }

    IEnumerator AttackSequence()
    {
        isAttacking = true;
        paused = true;

        // Reset all movement-related triggers
        ResetTriggers();

        // First Hook
        PlayerAnim.SetTrigger("FirstHook");

        // Wait for animation to progress
        yield return new WaitForSeconds(0.15f);

        // Wait for potential second attack input
        float waitTimer = 0f;
        bool secondAttackRequested = false;

        paused = false;
        while (waitTimer < 1f)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                secondAttackRequested = true;
                break;

            }

            waitTimer += Time.deltaTime;
            yield return null;
        }

        // Second Hook
        if (secondAttackRequested)
        {
            ResetTriggers();

            PlayerAnim.SetTrigger("SecondHook");
            paused = true;
        }

        
        // Wait for animation to progress
        yield return new WaitForSeconds(0.15f);
            
        // Wait for potential finisher input            
        waitTimer = 0f;            
        bool finisherRequested = false;

        paused = false;
        while (waitTimer < 1f)            
        {                
            if (Input.GetKeyDown(KeyCode.Mouse0))                
            {                    
                finisherRequested = true;                    
                break;
                
            }               
            waitTimer += Time.deltaTime;                
            yield return null;
            
        }
            
        // Finisher            
        if (finisherRequested)            
        {
            ResetTriggers();

            PlayerAnim.ResetTrigger("SecondHook");       
            PlayerAnim.SetTrigger("Finisher");
            paused = true;

        }


        // Reset state
        yield return new WaitForSeconds(0.1f);
        paused = false;
        isAttacking = false;
    }

}
