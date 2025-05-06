using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dash : MonoBehaviour {
    [Header("Dash Values")]
    public float Force = 10f;
    public int Cooldown = 2;

    [Header("References")]
    private Rigidbody PlayerRB;
    private PlayerMovement PlayerMovementScript;

    [Header("Other")]
    private bool CanDash = true;

   

    private void Start()
    {  
        PlayerMovementScript = FindObjectOfType<PlayerMovement>();
        PlayerRB = FindObjectOfType<Rigidbody>();
    }

    void Update() {
        if (CanDash == true && (Keyboard.current.leftShiftKey.wasPressedThisFrame)) { StartCoroutine(Dashing()); }
    
    
    }

    IEnumerator Dashing() {
        PlayerMovementScript.IsDashing = true;
        CanDash = false;

        Vector3 Direction = transform.forward;
        PlayerRB.AddForce(Direction.normalized * Force, ForceMode.VelocityChange);

        yield return new WaitForSeconds(0.4f);
        PlayerMovementScript.IsDashing = false;

        yield return new WaitForSeconds(Cooldown - 0.4f);
        CanDash = true;
    }
}