using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public SmallCounter SmallCounterScript;
    public MedCounter MediumCounterScript;

    public EnemyHealthBar healthBar;
    public int maxHealth = 100;
    public int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            if(SmallCounterScript != null)
            {
                SmallCounterScript.Counter += 1;

            }
            else if (MediumCounterScript != null)
            {
                MediumCounterScript.Counter += 1;

            }
            Destroy(gameObject);
        }
    }
}

