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
    private PlayerMovement PlayerMovementScript;



    // Start is called before the first frame update
    void Start()
    {
        CurRoomChangeState = RoomChangeState.Idle;

        //Get Player Object and PlayerMovement Script
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerMovementScript = FindObjectOfType<PlayerMovement>();

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

    public IEnumerator SwitchRoom()
    {
        PlayerMovementScript.paused = true;
        Debug.Log("CoroutineStarted");
        yield return new WaitForSeconds(0.3f);
        if (DoorName == "Up")
        { 
            Debug.Log("Up");
            Player.transform.position += Vector3.forward * 50f;
        }

        if (DoorName == "Down")
        {
            Debug.Log("Down");
            Player.transform.position += Vector3.back * 50f;
        }

        if (DoorName == "Right")
        {
            Debug.Log("Right");
            Player.transform.position += Vector3.right * 50f;
        }

        if (DoorName == "Left")
        {
            Debug.Log("Left");
            Player.transform.position += Vector3.left * 50f;

        }
        PlayerMovementScript.paused = false;

    }

}
