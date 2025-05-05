using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDoDamage : MonoBehaviour{
    public int MinDamage;
    public int MaxDamage;
    public bool FriendlyFire;

    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        
    }

    void OnTriggerEnter(Collider collision){
        if(collision.gameObject.CompareTag("Player")){
            collision.GetComponent<PlayerHealth>().TakeDamage(Random.Range(MinDamage, MaxDamage + 1));
        }
        if(FriendlyFire){
            if(collision.gameObject.CompareTag("Mini Raton") || collision.gameObject.CompareTag("Raton")){
                collision.GetComponent<EnemyHealth>().TakeDamage(Random.Range(MinDamage, MaxDamage + 1));
            }
        }
    }
}
