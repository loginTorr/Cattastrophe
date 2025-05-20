using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boon : MonoBehaviour
{
    [Header("References")]
    private PlayerMovement PlayerMovementScript;
    public PlayerHealth playerHealth;
    public GameObject Boons, Door;
    public string ParentName;
    [Header("Misc")]
    public bool PlayerInRadius = false;

    void Awake() {
        PlayerMovementScript = FindObjectOfType<PlayerMovement>();
        playerHealth = FindObjectOfType<PlayerHealth>();
        Boons = GameObject.Find("Boons");  
        ParentName = transform.parent.name;

        Door.SetActive(false);
    }
    
    private void OnTriggerEnter(Collider Obj) {
        if (Obj.CompareTag("Player")) { PlayerInRadius = true; }
        Debug.Log("Entered");
    }

    private void OnTriggerExit(Collider Obj) {
        if (Obj.CompareTag("Player")) { PlayerInRadius = false; }
    }

    void Update() {
        if (PlayerInRadius && Input.GetKeyDown(KeyCode.E)) {
            if (ParentName == "ScratchingPost") { DmgStatIncrease(); }
            if (ParentName == "CatToy") { SpeedStatIncrease(); }
            if (ParentName == "Fish") { HealthStatIncrease();}

        }
    }

    void DmgStatIncrease() {
        PlayerMovementScript.AttackDamage += 2;
        Door.SetActive(true);
        Boons.SetActive(false);
    }

    void SpeedStatIncrease() {
        PlayerMovementScript.MaxSpeed += 1;
        Door.SetActive(true);
        Boons.SetActive(false);
    }

    void HealthStatIncrease() {
        playerHealth.AddHealth(10);
        Door.SetActive(true);
        Boons.SetActive(false);
    }
}
