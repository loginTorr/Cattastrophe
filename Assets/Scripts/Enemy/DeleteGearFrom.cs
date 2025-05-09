using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteGearFrom : MonoBehaviour{
    private void OnTriggerExit(Collider other){
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Floor")){
            print("deleting gear");
            Destroy(this.gameObject);
        }
    }

}
