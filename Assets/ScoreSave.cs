using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSave : MonoBehaviour
{
    // Start is called before the first frame update
    public static ScoreSave instance;
    public int score = 0;
    void Awake()
    {
        // Singleton pattern to avoid duplicates
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
        }
    }
    
    public void SetScore(int newScore){
        score = newScore;
    }
}
