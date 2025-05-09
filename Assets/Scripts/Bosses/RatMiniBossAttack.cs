using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatMiniBossAttack : MonoBehaviour
{
    private RatMiniBoss RatMiniBossScript;
    private PlayerMovement PlayerMovementScript;

    // Start is called before the first frame update
    void Start()
    {
        RatMiniBossScript = GetComponent<RatMiniBoss>();
        PlayerMovementScript = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerExit(Collider other)
    {

        // Check if the collided object is an enemy
        if (RatMiniBossScript.isAttacking)
        {
            if (other.CompareTag("Player"))
            {
                // Try to get the Enemy Health Component
                PlayerMovementScript.CurHealth -= 10;


            }
        }
    }
}
