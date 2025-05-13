using System.Collections;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    [Header("Misc")]
    public int Timer = 0;
    public int MaxScore = 10000;
    public int Score = 0;

    void Start()
    {
        StartCoroutine(AddToTimer());
    }

    public void AddScore(int PlusScore) {
        Score += PlusScore;
    }

    IEnumerator AddToTimer() {
        while (true){ 
            yield return new WaitForSeconds(1);
            Timer += 1;
            Score = MaxScore - Timer;
            Debug.Log("score:" + Score);
        }
    }

}
