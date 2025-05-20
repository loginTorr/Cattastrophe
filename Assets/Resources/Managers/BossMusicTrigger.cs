using UnityEngine;

public class BossMusicOnSpawn : MonoBehaviour
{
    void OnEnable()
    {
        Invoke("StartBossMusic", 0.5f);
    }

    void StartBossMusic()
    {
        MusicManager musicManager = MusicManager.Instance;

        if (musicManager != null)
        {
            musicManager.PlayBossMusic();
        }
        else
        {
            Debug.LogWarning("MusicManager not found. Boss music won't play.");
        }
    }
}