using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundFXManager : MonoBehaviour
{
    #region Singleton
    public static SoundFXManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeAudioPools();
            LoadVolumeSettings();
        }
        else
        {
            Debug.LogWarning("Multiple SoundFXManager instances detected. Destroying duplicate.");
            Destroy(gameObject);
        }
    }
    #endregion

    #region Inspector Fields
    [Header("Audio Sources")]
    [SerializeField] private AudioSource soundFXPrefab;
    [SerializeField] private int initialPoolSize = 20;

    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private string masterVolumeParam = "MasterVolume";
    [SerializeField] private string sfxVolumeParam = "SFXVolume";
    [SerializeField] private string musicVolumeParam = "MusicVolume";
    [SerializeField] private string uiVolumeParam = "UIVolume";

    [Header("Settings")]
    [SerializeField] private int maxSimultaneousSounds = 20;
    [SerializeField] private bool debugMode = false;
    #endregion

    #region Private Variables
    private float master_volume = 1f;
    private float sfx_volume = 1f;
    private float music_volume = 1f;
    private float ui_volume = 1f;

    private Queue<AudioSource> audio_pool;
    private List<AudioSource> active_sources;
    private Dictionary<AudioSource, Coroutine> fade_coroutines;
    private Dictionary<int, AudioSource> looping_audio;

    private int curr_sound_count = 0;
    private int sound_id = 0;
    #endregion

    #region Properties
    public float MasterVolume
    {
        get { return master_volume; }
        set
        {
            master_volume = Mathf.Clamp01(value);
            UpdateMixerVolume(masterVolumeParam, master_volume);
            PlayerPrefs.SetFloat("MasterVolume", master_volume);
            PlayerPrefs.Save();
        }
    }

    public float SFXVolume
    {
        get { return sfx_volume; }
        set
        {
            sfx_volume = Mathf.Clamp01(value);
            UpdateMixerVolume(sfxVolumeParam, sfx_volume);
            PlayerPrefs.SetFloat("SFXVolume", sfx_volume);
            PlayerPrefs.Save();
        }
    }

    public float MusicVolume
    {
        get { return music_volume; }
        set
        {
            music_volume = Mathf.Clamp01(value);
            UpdateMixerVolume(musicVolumeParam, music_volume);
            PlayerPrefs.SetFloat("MusicVolume", music_volume);
            PlayerPrefs.Save();
        }
    }

    public float UIVolume
    {
        get { return ui_volume; }
        set
        {
            ui_volume = Mathf.Clamp01(value);
            UpdateMixerVolume(uiVolumeParam, ui_volume);
            PlayerPrefs.SetFloat("UIVolume", ui_volume);
            PlayerPrefs.Save();
        }
    }
    #endregion

    #region Initialization
    private void InitializeAudioPools()
    {
        audio_pool = new Queue<AudioSource>();
        active_sources = new List<AudioSource>();
        fade_coroutines = new Dictionary<AudioSource, Coroutine>();
        looping_audio = new Dictionary<int, AudioSource>();

        for (int i = 0; i < initialPoolSize; i++)
        {
            AudioSource source = CreatePooledAudioSource();
            audio_pool.Enqueue(source);
        }
    }

    private AudioSource CreatePooledAudioSource()
    {
        AudioSource source = Instantiate(soundFXPrefab, transform);
        source.gameObject.SetActive(false);
        return source;
    }

    private void LoadVolumeSettings()
    {
        master_volume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        sfx_volume = PlayerPrefs.GetFloat("SFXVolume", 1f);
        music_volume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        ui_volume = PlayerPrefs.GetFloat("UIVolume", 1f);

        UpdateMixerVolume(masterVolumeParam, master_volume);
        UpdateMixerVolume(sfxVolumeParam, sfx_volume);
        UpdateMixerVolume(musicVolumeParam, music_volume);
        UpdateMixerVolume(uiVolumeParam, ui_volume);
    }

    private void UpdateMixerVolume(string paramName, float volume)
    {
        if (audioMixer != null)
        {
            float dbValue = volume > 0.001f ? 20f * Mathf.Log10(volume) : -80f;
            audioMixer.SetFloat(paramName, dbValue);
        }
    }
    #endregion

    #region Audio Source Management
    private AudioSource GetAudioSource(Vector3 position)
    {
        AudioSource source;

        if (audio_pool.Count > 0)
        {
            source = audio_pool.Dequeue();
        }
        else
        {
            source = CreatePooledAudioSource();
            LogDebug("Audio pool expanded - creating new audio source");
        }

        source.gameObject.SetActive(true);
        source.transform.position = position;
        source.Stop();
        source.loop = false;
        source.spatialBlend = 0f;
        source.pitch = 1f;
        source.volume = 1f;

        active_sources.Add(source);
        curr_sound_count++;

        return source;
    }

    private void ReturnAudioSourceToPool(AudioSource source)
    {
        if (active_sources.Contains(source))
        {
            active_sources.Remove(source);
            curr_sound_count--;

            source.Stop();
            source.clip = null;
            source.gameObject.SetActive(false);
            source.transform.SetParent(transform);
            source.transform.localPosition = Vector3.zero;

            audio_pool.Enqueue(source);
        }
    }

    private IEnumerator ReturnToPoolAfterPlay(AudioSource source)
    {
        while (source != null && source.isPlaying)
        {
            yield return null;
        }

        if (source != null)
        {
            ReturnAudioSourceToPool(source);
        }
    }

    private IEnumerator FadeAudioSource(AudioSource source, float startVolume, float endVolume, float duration, bool stopAfterFade = false)
    {
        float startTime = Time.time;
        float endTime = startTime + duration;
        float originalVolume = source.volume;

        source.volume = startVolume;

        while (Time.time < endTime && source != null)
        {
            float elapsed = Time.time - startTime;
            float t = elapsed / duration;
            source.volume = Mathf.Lerp(startVolume, endVolume, t);
            yield return null;
        }

        if (source != null)
        {
            source.volume = endVolume;

            if (stopAfterFade && endVolume < 0.01f)
            {
                source.Stop();
                ReturnAudioSourceToPool(source);
            }
        }

        if (fade_coroutines.ContainsKey(source))
        {
            fade_coroutines.Remove(source);
        }
    }

    private void LogDebug(string message)
    {
        if (debugMode)
        {
            Debug.Log($"[SoundFXManager] {message}");
        }
    }

    private void PrioritizeSound(AudioSource source, SoundCategory category)
    {
        if (curr_sound_count > maxSimultaneousSounds)
        {
            AudioSource oldestSource = null;

            foreach (AudioSource activeSource in active_sources)
            {
                if (!activeSource.loop && (oldestSource == null || activeSource.time > oldestSource.time))
                {
                    oldestSource = activeSource;
                }
            }

            if (oldestSource != null)
            {
                LogDebug("Max simultaneous sounds exceeded - stopping oldest sound");
                oldestSource.Stop();
                ReturnAudioSourceToPool(oldestSource);
            }
        }
    }
    #endregion

    #region Public Sound Methods
    public enum SoundCategory
    {
        SFX,
        Music,
        Ambient,
        UI
    }

    public AudioSource PlaySound(AudioClip clip, Vector3 position, SoundCategory category = SoundCategory.SFX,
        float volume = 1f, float pitch = 1f, float pitchRandomness = 0f, float volumeRandomness = 0f)
    {
        if (clip == null)
        {
            LogDebug("Attempted to play null audio clip");
            return null;
        }

        AudioSource source = GetAudioSource(position);

        float categoryVolume = 1f;
        switch (category)
        {
            case SoundCategory.SFX:
                categoryVolume = sfx_volume;
                break;
            case SoundCategory.Music:
                categoryVolume = music_volume;
                break;
            case SoundCategory.UI:
                categoryVolume = ui_volume;
                break;
        }

        float finalPitch = pitch;
        float finalVolume = volume;

        if (pitchRandomness > 0)
        {
            finalPitch += Random.Range(-pitchRandomness, pitchRandomness);
        }

        if (volumeRandomness > 0)
        {
            finalVolume *= (1f + Random.Range(-volumeRandomness, volumeRandomness));
            finalVolume = Mathf.Clamp01(finalVolume);
        }

        source.clip = clip;
        source.volume = finalVolume * categoryVolume * master_volume;
        source.pitch = finalPitch;
        source.spatialBlend = 0f;
        source.Play();

        PrioritizeSound(source, category);

        StartCoroutine(ReturnToPoolAfterPlay(source));

        return source;
    }

    public AudioSource CreatePersistentAudioSource(AudioClip clip, Vector3 position, SoundCategory category = SoundCategory.SFX,
        float volume = 1f, float pitch = 1f)
    {
        if (clip == null)
        {
            LogDebug("Attempted to create persistent audio source with null clip");
            return null;
        }

        AudioSource source = GetAudioSource(position);

        float categoryVolume = 1f;
        switch (category)
        {
            case SoundCategory.SFX:
                categoryVolume = sfx_volume;
                break;
            case SoundCategory.Music:
                categoryVolume = music_volume;
                break;
            case SoundCategory.UI:
                categoryVolume = ui_volume;
                break;
        }

        source.clip = clip;
        source.volume = volume * categoryVolume * master_volume;
        source.pitch = pitch;
        source.Play();

        return source;
    }

    public int PlayLoopingSound(AudioClip clip, Vector3 position, SoundCategory category = SoundCategory.Ambient,
        float volume = 1f, float pitch = 1f, float fadeInDuration = 0f)
    {
        if (clip == null)
        {
            LogDebug("Attempted to play looping sound with null clip");
            return -1;
        }

        AudioSource source = GetAudioSource(position);

        float categoryVolume = 1f;
        switch (category)
        {
            case SoundCategory.SFX:
                categoryVolume = sfx_volume;
                break;
            case SoundCategory.Music:
                categoryVolume = music_volume;
                break;
            case SoundCategory.UI:
                categoryVolume = ui_volume;
                break;
        }

        source.clip = clip;
        source.pitch = pitch;
        source.loop = true;

        if (fadeInDuration > 0f)
        {
            source.volume = 0f;
            source.Play();
            FadeSound(source, volume * categoryVolume * master_volume, fadeInDuration);
        }
        else
        {
            source.volume = volume * categoryVolume * master_volume;
            source.Play();
        }

        int soundId = sound_id++;
        looping_audio[soundId] = source;

        return soundId;
    }

    public void StopLoopingSound(int soundId, float fadeOutDuration = 0f)
    {
        if (looping_audio.TryGetValue(soundId, out AudioSource source))
        {
            if (fadeOutDuration > 0f && source != null)
            {
                FadeSound(source, 0f, fadeOutDuration, true);
            }
            else if (source != null)
            {
                source.Stop();
                ReturnAudioSourceToPool(source);
            }

            looping_audio.Remove(soundId);
        }
    }

    public AudioSource PlaySpatialSound(AudioClip clip, Vector3 position, SoundCategory category = SoundCategory.SFX,
        float volume = 1f, float pitch = 1f, float minDistance = 1f, float maxDistance = 20f,
        float pitchRandomness = 0f, float volumeRandomness = 0f)
    {
        if (clip == null)
        {
            LogDebug("Attempted to play spatial sound with null clip");
            return null;
        }

        AudioSource source = GetAudioSource(position);

        float categoryVolume = 1f;
        switch (category)
        {
            case SoundCategory.SFX:
                categoryVolume = sfx_volume;
                break;
            case SoundCategory.Music:
                categoryVolume = music_volume;
                break;
            case SoundCategory.UI:
                categoryVolume = ui_volume;
                break;
        }

        float finalPitch = pitch;
        float finalVolume = volume;

        if (pitchRandomness > 0)
        {
            finalPitch += Random.Range(-pitchRandomness, pitchRandomness);
        }

        if (volumeRandomness > 0)
        {
            finalVolume *= (1f + Random.Range(-volumeRandomness, volumeRandomness));
            finalVolume = Mathf.Clamp01(finalVolume);
        }

        source.clip = clip;
        source.volume = finalVolume * categoryVolume * master_volume;
        source.pitch = finalPitch;
        source.spatialBlend = 1f;
        source.minDistance = minDistance;
        source.maxDistance = maxDistance;
        source.rolloffMode = AudioRolloffMode.Logarithmic;
        source.Play();

        PrioritizeSound(source, category);

        StartCoroutine(ReturnToPoolAfterPlay(source));

        return source;
    }

    public AudioSource PlayRandomSound(AudioClip[] clips, Vector3 position, SoundCategory category = SoundCategory.SFX,
        float volume = 1f, float pitch = 1f, float pitchRandomness = 0f, float volumeRandomness = 0f)
    {
        if (clips == null || clips.Length == 0)
        {
            LogDebug("Attempted to play random sound with null or empty clip array");
            return null;
        }

        int randomIndex = Random.Range(0, clips.Length);
        AudioClip randomClip = clips[randomIndex];

        return PlaySound(randomClip, position, category, volume, pitch, pitchRandomness, volumeRandomness);
    }

    public void FadeSound(AudioSource source, float targetVolume, float duration, bool stopAfterFade = false)
    {
        if (source == null)
        {
            LogDebug("Attempted to fade null audio source");
            return;
        }

        if (fade_coroutines.TryGetValue(source, out Coroutine existingCoroutine))
        {
            StopCoroutine(existingCoroutine);
        }

        Coroutine fadeCoroutine = StartCoroutine(FadeAudioSource(source, source.volume, targetVolume, duration, stopAfterFade));
        fade_coroutines[source] = fadeCoroutine;
    }

    public void ReleasePersistentAudioSource(AudioSource source)
    {
        if (source != null)
        {
            ReturnAudioSourceToPool(source);
        }
    }

    public void StopAllSounds()
    {
        List<AudioSource> sourcesToStop = new List<AudioSource>(active_sources);

        foreach (AudioSource source in sourcesToStop)
        {
            source.Stop();
            ReturnAudioSourceToPool(source);
        }

        looping_audio.Clear();
    }

    public void PauseAllSounds()
    {
        foreach (AudioSource source in active_sources)
        {
            source.Pause();
        }
    }

    public void UnpauseAllSounds()
    {
        foreach (AudioSource source in active_sources)
        {
            source.UnPause();
        }
    }

    public void SaveVolumeSettings()
    {
        PlayerPrefs.SetFloat("MasterVolume", master_volume);
        PlayerPrefs.SetFloat("SFXVolume", sfx_volume);
        PlayerPrefs.SetFloat("MusicVolume", music_volume);
        PlayerPrefs.SetFloat("UIVolume", ui_volume);
        PlayerPrefs.Save();
    }

    public void ResetVolumeSettings()
    {
        MasterVolume = 1f;
        SFXVolume = 1f;
        MusicVolume = 1f;
        UIVolume = 1f;
    }
    #endregion
}