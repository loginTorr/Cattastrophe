using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    public PlayerMovement healthCheck;
    public TextMeshProUGUI health;
    public TextMeshProUGUI speed;
    public TextMeshProUGUI attack;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        health.text = "Health: " + healthCheck.CurHealth + "/" + healthCheck.MaxHealth;
        speed.text = "Speed: " + healthCheck.MaxSpeed;
        attack.text = "Attack: " + healthCheck.AttackDamage;

    }
}
