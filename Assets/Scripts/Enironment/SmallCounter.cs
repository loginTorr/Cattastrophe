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
    public HighScore ScoreScript;

    void Start()
    {
        Counter = 0;
        
        Boons = GameObject.Find("Boons");
        Boons.SetActive(false);

        ScoreScript = GameObject.Find("Player").GetComponent<HighScore>();

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
        ScoreScript.AddScore(50);
        // spawns powerups and unlocks doors
        Boons.SetActive(true);
    }
}
