using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dash : MonoBehaviour {
    [Header("References")]
    public Rigidbody PlayerRB;

    [Header("Dash Values")]
    public float Force = 10f;
    public int Cooldown = 4;

    [Header("Other")]
    public bool CanDash = true;
    void Update() {
        if (CanDash == true && (Keyboard.current.leftShiftKey.wasPressedThisFrame || Gamepad.current.buttonEast.wasPressedThisFrame)) { StartCoroutine(Dashing()); }
    }

    IEnumerator Dashing() {
        CanDash = false;

        Vector3 Direction = transform.forward;
        PlayerRB.AddForce(Direction.normalized * Force, ForceMode.VelocityChange);

        yield return new WaitForSeconds(Cooldown);
        CanDash = true;
    }
}