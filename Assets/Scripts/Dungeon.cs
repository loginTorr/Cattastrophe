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

    public bool switchingDungeons;
    public bool isRatDungeon;
    public bool isWolfDungeon;


    private PlayerMovement PlayerMovementScript;



    // Start is called before the first frame update
    void Start()
    {
        CurDungeonState = DungeonState.HomeWorld;
        switchingDungeons = false;
        PlayerMovementScript = FindObjectOfType<PlayerMovement>();
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

                case DungeonState.ExitDungeon:
                    break;

                case DungeonState.HomeWorld:
                    break;

                case DungeonState.RatDungeon:
                    break;

                case DungeonState.WolfDungeon:
                    break;
            }
            switchingDungeons = false;
        }
    }
    IEnumerator EnterDungeon()
    {
        InteractUI.SetActive(false);
        Debug.Log("CoroutineStarted");
        GameHub.SetActive(false);

        if (RatDungeon)
        {
            Debug.Log("RatDungeon");
            RatDungeon.SetActive(true);
        }

        else if (WolfDungeon)
        {
            Debug.Log("WolfDungeon");
            WolfDungeon.SetActive(true);
        }

        yield return new WaitForSeconds(2f);
        PlayerMovementScript.paused = false;
    }

}
