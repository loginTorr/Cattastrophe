using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallCounter : MonoBehaviour
{

    [Header("Number of Enemies")]
    public int Counter;

    [Header("PowerUps")]
    private GameObject Boons;

    [Header("Misc")]
    private bool CanEndRoom = true;
    // Start is called before the first frame update
    void Start()
    {
        Counter = 0;
        
        Boons = GameObject.Find("Boons");
        Boons.SetActive(false);

    }
    void Update()
    {
        if (Counter >= 5 && CanEndRoom) { 
            CanEndRoom = false;
            EndRoom(); 
        }
    }

    public void IncreaseCount()
    {
        Counter += 1;
    }

    void EndRoom()
    {
        Game.RoomCleared = true;
        // spawns powerups and unlocks doors
        Boons.SetActive(true);
    }
}
