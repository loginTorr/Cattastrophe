using System;
using System.Collections;
using UnityEngine;


public class InteractScript : MonoBehaviour
{
    public string ObjectName;
    public int curCount;

    private bool CanInteract = true;
    private bool InTrigger = false;

    private Dungeon DungeonScript;
    private RoomSpawner RoomSpawnerScript;
    private RoomRewards RoomRewardsScript;



    // Start is called before the first frame update
    void Start()
    {
        DungeonScript = FindObjectOfType<Dungeon>();
        RoomSpawnerScript = FindObjectOfType<RoomSpawner>();
        RoomRewardsScript = FindObjectOfType<RoomRewards>();

    }

    // Update is called once per frame
    void Update()
    {
        if (InTrigger && Input.GetKeyDown(KeyCode.E) && CanInteract)
        {
            StartCoroutine("Interact");
        }
    }

    private void OnTriggerEnter()
    {
        //Game.InteractUI.SetActive(true);
        ObjectName = gameObject.name;
        InTrigger = true;
        curCount = 0;
    }

    private void OnTriggerExit()
    {
        Game.InteractUI.SetActive(false);
        InTrigger = false;
        curCount = 1;

    }

    private IEnumerator Interact()
    {

        CanInteract = false;

        if (ObjectName == "RatDungeonPortal")
        {
            Debug.Log("RatPortal");
            Game.InteractUI.SetActive(false);
            CameraFade.fadeInstance.FadeOut();
            DungeonScript.isRatDungeon = true;
            DungeonScript.CurDungeonState = DungeonState.EnterDungeon;
            DungeonScript.switchingDungeons = true;
        }

        else if (ObjectName == "WolfDungeonPortal")
        {
            Debug.Log("WolfPortal");
            CameraFade.fadeInstance.FadeOut();
            DungeonScript.isWolfDungeon = true;
            DungeonScript.CurDungeonState = DungeonState.EnterDungeon;
            DungeonScript.switchingDungeons = true;

        }

        else if (gameObject.tag == "Door")
        {
            if (curCount < 1 && Game.RoomCleared)
            {
                Game.RoomCleared = false;
                CameraFade.fadeInstance.FadeOut();
                yield return new WaitForSeconds(1.3f);
                RoomSpawnerScript.StartCoroutine("SpawnNextRoom");
                DungeonScript.CurDungeonState = DungeonState.SwitchingRooms;
                DungeonScript.switchingDungeons = true;

            }
        }

        else if (gameObject.tag == "RoomReward")
        {
            RoomRewardsScript.RewardName = ObjectName;
            RoomRewardsScript.GrabReward();
        }


        yield return new WaitForSeconds(0.5f);
        CanInteract = true;

    }

}
