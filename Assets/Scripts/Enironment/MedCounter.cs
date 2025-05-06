using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedCounter : MonoBehaviour
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
        if (Counter >= 10) { EndRoom(); }
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
