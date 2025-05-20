using UnityEngine;

public class GameMusicStarter : MonoBehaviour
{
    void Start()
    {
        MusicManager musicManager = MusicManager.Instance;

        if (musicManager != null)
        {
            musicManager.PlayAmbientMusic();
        }
        else
        {
            Debug.LogWarning("MusicManager not found. Game music won't start.");
        }
    }
}