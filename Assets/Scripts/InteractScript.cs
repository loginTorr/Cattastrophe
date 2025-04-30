using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class InteractScript : MonoBehaviour
{
    public GameObject InteractUI;
    public string ObjectName;
    public int curCount;

    private bool CanInteract = true;
    private bool InTrigger = false;

    private Dungeon DungeonScript;
    private RoomChange RoomChangeScript;
    private RoomRewards RoomRewardsScript;




    // Start is called before the first frame update
    void Start()
    {
        DungeonScript = FindObjectOfType<Dungeon>();
        RoomChangeScript = FindObjectOfType<RoomChange>();
        RoomRewardsScript = FindObjectOfType<RoomRewards>();

    }

    // Update is called once per frame
    void Update()
    {
        if (InTrigger && Input.GetKeyDown(KeyCode.E) && CanInteract)
        {
            StartCoroutine(Interact());
        }
    }

    private void OnTriggerEnter()
    {
        ObjectName = gameObject.name;
        InTrigger = true;
        curCount = 0;
    }

    private void OnTriggerExit()
    {
        InteractUI.SetActive(false);
        InTrigger = false;
        curCount = 1;

    }

    private IEnumerator Interact()
    {

        CanInteract = false;

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

        else if (gameObject.tag == "Door")
        {
            if (curCount < 1)
            {
                RoomChangeScript.DoorName = ObjectName;
                RoomChangeScript.StartCoroutine("SwitchRoom");

            }
        }

        else if (gameObject.tag == "RoomReward")
        {
            RoomRewardsScript.RewardName = ObjectName;
            RoomRewardsScript.GrabReward();
        }


        yield return new WaitForSeconds(0.3f);

        CanInteract = true;

    }

}
