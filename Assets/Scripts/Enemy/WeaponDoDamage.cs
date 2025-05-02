using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDoDamage : MonoBehaviour{
    public bool FriendlyFire;
    private bool collided = false;

    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        
    }

    void OnTriggerEnter(Collider collision){
        if(collision.gameObject.CompareTag("Player")){
            // do damage to the player
        }
        if(FriendlyFire){
            if(collision.gameObject.CompareTag("Mini Raton") || collision.gameObject.CompareTag("Raton")){
                // do damage to the enemy
            }
        }
        collided = true;
    }

    // if the moment of collision fell between frames,
    // this will catch the collision on a Stay
    void OnTriggerStay(Collider collision){
        if (collided == false) { 
            if(collision.gameObject.CompareTag("Player")){
                // do damage to the player
            }
            if(FriendlyFire){
                if(collision.gameObject.CompareTag("Mini Raton") || collision.gameObject.CompareTag("Raton")){
                    // do damage to the enemy
                }
            }
            collided = true;
        }
    }

    void OnTriggerExit(Collider collision){
        collided = false;
    }
}
