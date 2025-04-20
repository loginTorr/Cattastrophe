using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class InteractScript : MonoBehaviour
{
    public GameObject InteractUI;

    private Dungeon DungeonScript;



    // Start is called before the first frame update
    void Start()
    {
        DungeonScript = FindObjectOfType<Dungeon>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay()
    {
        InteractUI.SetActive(true);

        if (Input.GetKeyDown(KeyCode.E))
        {
            var ObjectName = gameObject.name;
            Debug.Log("E Pressed");

            if (ObjectName == "RatDungeonPortal")
            {
                Debug.Log("RatPortal");
                DungeonScript.CurDungeonState = DungeonState.EnterDungeon;
                DungeonScript.switchingDungeons = true;
            }

            if (ObjectName == "WolfDungeonPortal")
            {
                Debug.Log("WolfPortal");
                DungeonScript.CurDungeonState = DungeonState.EnterDungeon;
                DungeonScript.switchingDungeons = true;

            }
        }
    }

    private void OnTriggerExit()
    {
        InteractUI.SetActive(false);
    }

}
