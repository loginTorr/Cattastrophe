using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YarnAttack : MonoBehaviour {
    [Header("References")]
    public GameObject AttackObject;
    public Rigidbody rb;
    public GameObject ExplosionRadius;
    public GameObject Barrel;
    [Header("Attack Stats")]
    public float Duration = 4.0f;
    public int Dmg = 1;

    void Start()
    {
        Destroy(AttackObject, Duration);
        if (GameObject.Find("Barrel") != null) { 
                Barrel = GameObject.Find("Barrel");
                ExplosionRadius = GameObject.Find("ExplosionRadius");
                ExplosionRadius.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
        {

            // Check if the collided object is an enemy
            if (other.CompareTag("Raton") || other.CompareTag("Mini Raton") || other.CompareTag("RatMiniBoss"))
            {
                // Try to get the Enemy Health Component
                EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();

                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(Dmg);
                }


            }
            if (other.CompareTag("Barrel")) { StartCoroutine(Explosion()); }   
        }

        IEnumerator Explosion() {
            // wait however long the timer is
            yield return new WaitForSeconds(2);
            ExplosionRadius.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            Barrel.SetActive(false);

        }
}
