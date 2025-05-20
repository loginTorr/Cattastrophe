using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCount : MonoBehaviour
{
    public int Counter = 0;
    public int Timer = 15;
    private bool CanEndRoom = true;
    private GameHighScore ScoreScript;

    // Start is called before the first frame update
    void Start()
    {
        ScoreScript = GameObject.Find("Player").GetComponent<GameHighScore>();
        StartCoroutine(RoomTimer());
    }

    // Update is called once per frame
    void Update()
    {
        if (Counter >= 1 && CanEndRoom)
        {
            CanEndRoom = false;
            EndRoom();
        }
    }

    public void IncreaseCount()
    {
        Counter += 1;
    }

    void EndRoom()
    {
        Game.RoomCleared = true;
        ScoreScript.AddScore(100);
        // spawns powerups and unlocks doors
    }

    IEnumerator RoomTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            Timer -= 1;
            if (Timer <= 0) { yield break; }
        }
    }

}
