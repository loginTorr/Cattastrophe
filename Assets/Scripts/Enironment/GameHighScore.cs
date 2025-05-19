using System.Collections;
using UnityEngine;

public class GameHighScore : MonoBehaviour
{
    [Header("Misc")]
    public int Timer = 0;
    public int MaxScore = 10000;
    public int Score = 0;
    public int TimerScore = 0;

    void Start()
    {
        StartCoroutine(AddToTimer());
    }

    public void AddScore(int PlusScore) {
        Score += PlusScore;
    }

    public void BeatGame() {
        Score += TimerScore;
    }

    IEnumerator AddToTimer() {
        while (true){ 
            yield return new WaitForSeconds(1);
            Timer += 1;
            TimerScore = MaxScore - Timer;
        }
    }

}
