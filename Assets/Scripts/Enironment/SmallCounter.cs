using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallCounter : MonoBehaviour
{

    [Header("Number of Enemies")]
    public int Counter;

    [Header("PowerUps")]
    private GameObject fish;
    private GameObject CatToy;
    private GameObject ScratchingPost;
    // Start is called before the first frame update
    void Start()
    {
        Counter = 0;
        
        fish = GameObject.Find("Fish");
        CatToy = GameObject.Find("CatToy");
        ScratchingPost = GameObject.Find("ScratchingPost");
        fish.SetActive(false);
        CatToy.SetActive(false);
        ScratchingPost.SetActive(false);

    }
    void Update()
    {
        if (Counter >= 5) { EndRoom(); }
    }

    public void IncreaseCount()
    {
        Counter += 1;
    }

    void EndRoom()
    {
        Game.RoomCleared = true;
        // spawns powerups and unlocks doors
        fish.SetActive(true);
        CatToy.SetActive(true);
        ScratchingPost.SetActive(true);
    }
}
