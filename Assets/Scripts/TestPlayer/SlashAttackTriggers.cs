using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class SlashAttackTriggers : MonoBehaviour
    {
        public GameObject ExplosionRadius;
        public GameObject Barrel;

        public bool canDealDamage = false;
        private HashSet<Collider> hitEnemies = new HashSet<Collider>();


        public int damage;
        private PlayerMovement PlayerMovementScipt;
        private PlayerAttackController PlayerAttackControllerScript;


        // Start is called before the first frame update
        void Start()
        {
            PlayerMovementScipt = GetComponentInParent<PlayerMovement>();
            if (GameObject.Find("ExplodeBarrel") != null) { 
                Barrel = GameObject.Find("ExplodeBarrel");
                ExplosionRadius = GameObject.Find("ExplosionRadius");
                ExplosionRadius.SetActive(false);
            }

            PlayerAttackControllerScript = GetComponentInParent<PlayerAttackController>();
        }

        // Update is called once per frame
        void Update()
        {
            damage = PlayerAttackControllerScript.attackDamage;
        }

    
        public void EnableDamage()
        {
            canDealDamage = true;
            hitEnemies.Clear();
        }
        public void DisableDamage()
        {
            canDealDamage = false;
        }


    void OnTriggerEnter(Collider other)
    {

        //Debug.Log($"canDealDamage={canDealDamage} other.tag={other.tag} hitPlayersContains={hitEnemies.Contains(other)}");

        if (canDealDamage && !hitEnemies.Contains(other) && (other.CompareTag("Raton") || other.CompareTag("Mini Raton")))
        {
            // Try to get the Enemy Health Component
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
            hitEnemies.Add(other);


        }

        else if (canDealDamage && !hitEnemies.Contains(other) && other.CompareTag("RatMiniBoss"))
        {
            RatMiniBoss MiniBossHP = other.GetComponent<RatMiniBoss>();
            MiniBossHP.RatBossHealth -= damage;
            hitEnemies.Add(other);

        }

        else if (canDealDamage && !hitEnemies.Contains(other) && other.CompareTag("RatBoss"))
        {
            RatBoss ratBossHP = other.GetComponent<RatBoss>();
            ratBossHP.RatBossHealth -= damage;
            hitEnemies.Add(other);
        }




        else if (canDealDamage && !hitEnemies.Contains(other) && other.CompareTag("Barrel")) { StartCoroutine(Explosion()); }

    }

        IEnumerator Explosion() {
            // wait however long the timer is
            yield return new WaitForSeconds(2);
            ExplosionRadius.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            Barrel.SetActive(false);

        }

    }
