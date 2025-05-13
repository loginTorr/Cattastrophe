using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallCounter : MonoBehaviour
{

    [Header("Enemies")]
    public int Counter = 0;
    public int Timer = 15;
    public GameObject Wave2;

    [Header("PowerUps")]
    private GameObject Boons;

    [Header("Misc")]
    private bool CanEndRoom = true;
    public HighScore ScoreScript;

    void Start()
    {
        Wave2 = GameObject.Find("Wave2");
        Wave2.SetActive(false);
        Boons = GameObject.Find("Boons");
        Boons.SetActive(false);

        ScoreScript = GameObject.Find("Player").GetComponent<HighScore>();

        StartCoroutine(RoomTimer());
    }
    void Update()
    {
        if (Timer <= 0 || Counter >= 5) { Wave2.SetActive(true); }
        if (Counter >= 10 && CanEndRoom) { 
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

    IEnumerator RoomTimer() {
        while (true) {
            yield return new WaitForSeconds(1);
            Timer -= 1;
            if (Timer <= 0) { yield break; }
        }
    }
}
