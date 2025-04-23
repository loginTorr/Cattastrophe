using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour {
    [Header("References")]
    public Rigidbody PlayerRB;

    [Header("Dash Values")]
    public float Force = 10f;
    public int Cooldown = 1;

    [Header("Other")]
    public bool CanDash = true;
    void Update() {
        if (CanDash == true && Input.GetKeyDown(KeyCode.LeftShift)) { StartCoroutine(Dashing()); }
    }

    IEnumerator Dashing() {
        CanDash = false;

        Vector3 Direction = transform.forward;
        PlayerRB.AddForce(Direction.normalized * Force, ForceMode.VelocityChange);

        yield return new WaitForSeconds(Cooldown);
        CanDash = true;
    }
}