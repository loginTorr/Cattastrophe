using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YarnAttack : MonoBehaviour {
    [Header("References")]
    public GameObject AttackObject;
    public Rigidbody rb;
    [Header("Attack Stats")]
    public float Duration = 4.0f;
    public float Dmg = 1.0f;

    void Start()
    {
        Destroy(AttackObject, Duration);
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
