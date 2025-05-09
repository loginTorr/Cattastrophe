    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class SlashAttackTriggers : MonoBehaviour
    {
        public bool canDealDamage = false;

        public int damage;
        private PlayerMovement PlayerMovementScipt;

        


        // Start is called before the first frame update
        void Start()
        {
            PlayerMovementScipt = GetComponent<PlayerMovement>();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        void OnTriggerExit(Collider other)
        {

            // Check if the collided object is an enemy
            if (PlayerMovementScipt.isAttacking)
            {
                if (other.CompareTag("Raton") || other.CompareTag("Mini Raton") || other.CompareTag("RatMiniBoss"))
                {
                    // Try to get the Enemy Health Component
                    EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();

                    enemyHealth.TakeDamage(damage);


                }
            }   
        }

    }
