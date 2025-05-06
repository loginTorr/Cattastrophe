    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class SlashAttackTriggers : MonoBehaviour
    {
        public bool canDealDamage = false;

        public int damage;
        public PlayerMovement PlayerMovementScipt;

        


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            
        }

        void OnTriggerEnter(Collider other)
        {

            // Check if the collided object is an enemy
            if (PlayerMovementScipt.isAttacking)
            {
                if (other.CompareTag("Raton") || other.CompareTag("Mini Raton"))
                {
                    // Try to get the Enemy Health Component
                    EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();

                    enemyHealth.TakeDamage(damage);


                }
            }   
        }

    }
