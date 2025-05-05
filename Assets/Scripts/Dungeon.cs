using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    public DungeonState CurDungeonState;

    public GameObject InteractUI;
    public GameObject GameHub;
    public GameObject RatDungeon;
    public GameObject WolfDungeon;

    public Transform RatDungeonStartingPos;

    public bool switchingDungeons;
    public bool isRatDungeon;
    public bool isWolfDungeon;

    private GameObject Player;
    private PlayerMovement PlayerMovementScript;
    private RoomSpawner RoomSpawnerScript;




    // Start is called before the first frame update
    void Start()
    {
        CurDungeonState = DungeonState.HomeWorld;
        switchingDungeons = false;
        PlayerMovementScript = FindObjectOfType<PlayerMovement>();


        Player = GameObject.FindGameObjectWithTag("Player");
        RoomSpawnerScript = FindObjectOfType<RoomSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (switchingDungeons)
        {
            switch (CurDungeonState)
            {
                case DungeonState.EnterDungeon:
                    PlayerMovementScript.paused = true;
                    Debug.Log("EnterDungeon");
                    StartCoroutine(EnterDungeon());
                    if (isRatDungeon)
                    {
                        CurDungeonState = DungeonState.RatDungeon;
                    }

                    if (isWolfDungeon)
                    {
                        CurDungeonState = DungeonState.WolfDungeon;
                    }
                    break;

                case DungeonState.SwitchingRooms:
                    PlayerMovementScript.paused = true;
                    break;

                case DungeonState.ExitDungeon:
                    break;

                case DungeonState.HomeWorld:
                    break;

                case DungeonState.RatDungeon:
                    Debug.Log("RatDungeon");
                    break;

                case DungeonState.WolfDungeon:
                    Debug.Log("WolfDungeon");
                    break;
            }
            switchingDungeons = false;
        }
    }
    IEnumerator EnterDungeon()
    {
        InteractUI.SetActive(false);
        Debug.Log("CoroutineStarted");

        yield return new WaitForSeconds(1.5f);
        GameHub.SetActive(false);



        if (isRatDungeon)
        {
            Debug.Log("RatDungeon");
            Player.transform.position = RatDungeonStartingPos.position;
            RatDungeon.SetActive(true);
            RoomSpawnerScript.lastRoom = RatDungeon;
        }

        else if (isWolfDungeon)
        {
            Debug.Log("WolfDungeon");
            WolfDungeon.SetActive(true);
        }

        CameraFade.fadeInstance.FadeIn();
        yield return new WaitForSeconds(1f);
        PlayerMovementScript.paused = false;
    }

}
