using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class RoomChange : MonoBehaviour
{

    public string DoorName;

    private RoomChangeState CurRoomChangeState;
    private GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        CurRoomChangeState = RoomChangeState.Idle;
        Player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {

        switch (CurRoomChangeState)
        {
            case RoomChangeState.Idle:
                break;

            case RoomChangeState.RoomChanging: 
                break;

            case RoomChangeState.RoomChanged: 
                break;
        }

    }

    public void SwitchRoom()
    {
        Debug.Log("CoroutineStarted");
        if (DoorName == "Up")
        { 
            Debug.Log("Up");
            Player.transform.position += Vector3.forward * 50f;
        }

        else if (DoorName == "Down")
        {
            Debug.Log("Down");
            Player.transform.position += Vector3.back * 50f;
        }

        else if (DoorName == "Right")
        {
            Debug.Log("Right");
            Player.transform.position += Vector3.right * 50f;
        }

        else if (DoorName == "Left")
        {
            Debug.Log("Left");
            Player.transform.position += Vector3.left * 50f;

        }
        
    }

}
