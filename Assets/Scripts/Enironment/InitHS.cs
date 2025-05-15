using System.Collections;
using UnityEngine;
using HighScore;

public class InitHs : MonoBehaviour
{ 

    void Start() {
        HS.Init(this, "Cattastrophe");
    }


}