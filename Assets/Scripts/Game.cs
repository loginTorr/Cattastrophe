using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DungeonState { EnterDungeon, SwitchingRooms, ExitDungeon, HomeWorld, RatDungeon, WolfDungeon };
public enum RoomChangeState { Idle, RoomChanging, RoomChanged }

public class Game : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("GameStart");
        CameraFade.fadeInstance.FadeIn();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
