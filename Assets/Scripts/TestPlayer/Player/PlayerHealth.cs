using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public HealthBar healthBar;
    public GameObject gameOverScreen;

    private PlayerMovement PlayerMovmentScript;
    private float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        PlayerMovmentScript = GetComponent<PlayerMovement>();

        currentHealth = PlayerMovmentScript.MaxHealth;
        healthBar.SetMaxHealth(PlayerMovmentScript.MaxHealth);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        PlayerMovmentScript.CurHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth == 0){
            print("lol you died");
            GameOver();
        }
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
                healthBar.SetHealth(PlayerMovmentScript.MaxHealth);
                currentHealth = PlayerMovmentScript.MaxHealth;
            }
        }
    }
    
    public void GameOver(){
        print("game over");
        gameOverScreen.SetActive(true);
    }
}
