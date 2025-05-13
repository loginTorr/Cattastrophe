using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomRewards : MonoBehaviour
{
    private PlayerMovement PlayerMovementScript;

    public string RewardName;


    // Start is called before the first frame update
    void Start()
    {
        PlayerMovementScript = FindObjectOfType<PlayerMovement>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GrabReward()
    {
        if (RewardName == "ScratchingPost")
        {
            PlayerMovementScript.AttackDamage += 5;
            Debug.Log("dmg raised");
        }
        

        else if (RewardName == "Fish")
        {
            PlayerMovementScript.MaxHealth += 10;
            PlayerMovementScript.CurHealth += 10;
            Debug.Log("health raised");
        }

        else if (RewardName == "CatToy")
        {
            PlayerMovementScript.MaxSpeed += 1;
            Debug.Log("speed raised");
        }
        Destroy(gameObject);
    }
}
