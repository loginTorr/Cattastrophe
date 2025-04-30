using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAttack : MonoBehaviour {
    [Header("References")]
    public GameObject AttackObject;
    public Rigidbody rb;
    [Header("Attack Stats")]
    public float Duration = 4.0f;
    public float Dmg = 1.0f;
    public float Force = 10f;

    void Start()
    {
        Destroy(AttackObject, Duration);
        Vector3 Direction = transform.forward;
        rb.AddForce(Direction.normalized * Force, ForceMode.VelocityChange);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // cause damage to enemies
            return;
        }
    }
}
