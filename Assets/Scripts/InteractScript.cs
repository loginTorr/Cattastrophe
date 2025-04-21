using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class InteractScript : MonoBehaviour
{
    public GameObject InteractUI;

    private Dungeon DungeonScript;
    private RoomChange RoomChangeScript;

    string ObjectName;



    // Start is called before the first frame update
    void Start()
    {
        DungeonScript = FindObjectOfType<Dungeon>();
        RoomChangeScript = FindObjectOfType<RoomChange>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter()
    {
        ObjectName = gameObject.name;
    }

    private void OnTriggerStay()
    {
        InteractUI.SetActive(true);

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E Pressed");

            if (ObjectName == "RatDungeonPortal")
            {
                Debug.Log("RatPortal");
                DungeonScript.isRatDungeon = true;
                DungeonScript.CurDungeonState = DungeonState.EnterDungeon;
                DungeonScript.switchingDungeons = true;
            }

            else if (ObjectName == "WolfDungeonPortal")
            {
                Debug.Log("WolfPortal");
                DungeonScript.isWolfDungeon = true;
                DungeonScript.CurDungeonState = DungeonState.EnterDungeon;
                DungeonScript.switchingDungeons = true;

            }

            if (gameObject.tag == "Door")
            {
                RoomChangeScript.DoorName = ObjectName;
                RoomChangeScript.SwitchRoom();

            }
        }
    }

    private void OnTriggerExit()
    {
        InteractUI.SetActive(false);
    }

}
