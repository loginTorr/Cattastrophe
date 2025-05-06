using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DungeonState { EnterDungeon, SwitchingRooms, ExitDungeon, HomeWorld, RatDungeon, WolfDungeon };
public enum RoomChangeState { Idle, RoomChanging, RoomChanged }

public class Game : MonoBehaviour
{
    public static Boolean RoomCleared = true;
    public static GameObject InteractUI;



    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("GameStart");
        InteractUI = GameObject.Find("Interact");
        InteractUI.SetActive(false);
        CameraFade.fadeInstance.FadeIn();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
