using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public HealthBar healthBar;
    public PlayerMovement player;
    private float maxHealth;
    private float currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = player.MaxHealth;
        healthBar.SetMaxHealth(player.MaxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("RatBoss")){
            print("entered");
            TakeDamage(30);
        }
    }

    public void OnTriggerEnter(Collider collider){
        if(collider.gameObject.CompareTag("RatBoss")){
            TakeDamage(20);
        }
        
    }
    public void OnTriggerStay(Collider collider){
        if(collider.gameObject.CompareTag("Bed")){
            if(Input.GetKey(KeyCode.E)){
                healthBar.SetHealth(player.MaxHealth);
                currentHealth = player.MaxHealth;
            }
        }
    }
}
