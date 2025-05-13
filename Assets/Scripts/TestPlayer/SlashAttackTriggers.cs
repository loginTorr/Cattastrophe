    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class SlashAttackTriggers : MonoBehaviour
    {
        public GameObject ExplosionRadius;
        public GameObject Barrel;

        public bool canDealDamage = false;

        public int damage;
        private PlayerMovement PlayerMovementScipt;

        


        // Start is called before the first frame update
        void Start()
        {
            PlayerMovementScipt = GetComponentInParent<PlayerMovement>();
            if (GameObject.Find("Barrel") != null) { 
                Barrel = GameObject.Find("Barrel");
                ExplosionRadius = GameObject.Find("ExplosionRadius");
                ExplosionRadius.SetActive(false);
            }
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
                if (other.CompareTag("Raton") || other.CompareTag("Mini Raton") || other.CompareTag("RatMiniBoss"))
                {
                    // Try to get the Enemy Health Component
                    EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();

                    if (enemyHealth != null)
                    {
                        enemyHealth.TakeDamage(damage);
                    }


                }
                if (other.CompareTag("Barrel")) { StartCoroutine(Explosion()); }
            }   
        }

        IEnumerator Explosion() {
            // wait however long the timer is
            yield return new WaitForSeconds(2);
            ExplosionRadius.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            Barrel.SetActive(false);

        }

    }
