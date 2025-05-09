using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDoKnockback : MonoBehaviour{
    public float KnockbackStrength;
    private Vector3 direction;

    private void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag("Player")) {
            direction = Vector3.Normalize(other.gameObject.transform.position - transform.position);
            other.gameObject.GetComponent<PlayerMovement>().isGettingKockedBack = true;
            other.gameObject.GetComponent<PlayerMovement>().RecieveKockback(KnockbackStrength, direction);
        }
    }

    private void OnTriggerStay(Collider other){
        if (other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<PlayerMovement>().RecieveKockback(KnockbackStrength, direction);
        }
    }

    private void OnTriggerExit(Collider other){
        if (other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<PlayerMovement>().isGettingKockedBack = false;
        }
    }
}
