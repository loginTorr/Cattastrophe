using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallCounter : MonoBehaviour
{
    [Header("Number of Enemies")]
    public int Counter;
    // Start is called before the first frame update
    void Start()
    {
        Counter = 0;
    }
    void Update()
    {
        if (Counter >= 5) { EndRoom(); }
    }

    public void IncreaseCount()
    {
        Counter += 1;
    }

    void EndRoom()
    {
        // spawns powerups and unlocks doors
    }
}
