using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    #region Singleton
    public static MusicManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    [Header("Music Tracks")]
    [SerializeField] private AudioClip ambientMusic;
    [SerializeField] private AudioClip combatMusic;
    [SerializeField] private AudioClip bossMusic;
    [SerializeField] private AudioClip victoryMusic;

    [Header("Transition Settings")]
    [SerializeField] private float crossFadeDuration = 2.0f;
    [SerializeField] private float musicVolume = 0.5f;

    private SoundFXManager soundManager;
    private int currentMusicId = -1;
    private int queuedMusicId = -1;
    private MusicState currentState = MusicState.None;
    private Coroutine transitionCoroutine;

    public enum MusicState
    {
        None,
        Ambient,
        Combat,
        Boss,
        Victory
    }

    private void Start()
    {
        soundManager = SoundFXManager.Instance;
        if (soundManager == null)
        {
            Debug.LogError("SoundFXManager not found!");
            return;
        }

        PlayAmbientMusic();
    }

    public void PlayAmbientMusic()
    {
        TransitionToMusic(ambientMusic, MusicState.Ambient);
    }

    public void PlayCombatMusic()
    {
        TransitionToMusic(combatMusic, MusicState.Combat);
    }

    public void PlayBossMusic()
    {
        TransitionToMusic(bossMusic, MusicState.Boss);
    }

    public void PlayVictoryMusic()
    {
        TransitionToMusic(victoryMusic, MusicState.Victory);
    }

    private void TransitionToMusic(AudioClip newTrack, MusicState newState)
    {
        if (currentState == newState || newTrack == null)
            return;

        if (transitionCoroutine != null)
            StopCoroutine(transitionCoroutine);

        if (currentMusicId != -1)
        {
            transitionCoroutine = StartCoroutine(CrossFadeMusic(newTrack, newState));
        }
        else
        {
            currentMusicId = soundManager.PlayLoopingSound(
                newTrack,
                transform.position,
                SoundFXManager.SoundCategory.Music,
                musicVolume,
                1.0f
            );
            currentState = newState;
        }
    }

    private IEnumerator CrossFadeMusic(AudioClip newTrack, MusicState newState)
    {
        queuedMusicId = soundManager.PlayLoopingSound(
            newTrack,
            transform.position,
            SoundFXManager.SoundCategory.Music,
            0f,
            1.0f
        );

        float timeElapsed = 0;

        while (timeElapsed < crossFadeDuration)
        {
            timeElapsed += Time.deltaTime;
            float normalizedTime = timeElapsed / crossFadeDuration;

            soundManager.FadeSoundById(currentMusicId, musicVolume * (1 - normalizedTime), 0.1f);
            soundManager.FadeSoundById(queuedMusicId, musicVolume * normalizedTime, 0.1f);

            yield return null;
        }

        soundManager.StopLoopingSound(currentMusicId);

        currentMusicId = queuedMusicId;
        queuedMusicId = -1;
        currentState = newState;
        transitionCoroutine = null;
    }
}