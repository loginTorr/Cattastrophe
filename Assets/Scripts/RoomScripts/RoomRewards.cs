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
        }
        

        else if (RewardName == "Fish")
        {
            PlayerMovementScript.CurHealth += 10;
            PlayerMovementScript.MaxHealth += 10;
        }

        else if (RewardName == "CatToy")
        {
            PlayerMovementScript.MaxSpeed += 1;
        }
        Destroy(gameObject);
    }
}
