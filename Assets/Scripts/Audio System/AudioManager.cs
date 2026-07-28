using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Singleton
    public static AudioManager Instance { get; private set; } = null;
    void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);

        Instance = this;
        DontDestroyOnLoad(Instance);
    }
    #endregion
    Dictionary<AudioEffect, AudioSource> loadedAudioSources = new Dictionary<AudioEffect, AudioSource>();
    Queue<AudioSource> sourcePool = new Queue<AudioSource>();
    List<AudioSource> activeAudioSources = new List<AudioSource>();
    void OnEnable()
    {
        EventBus.Subscribe<PlayAudioEvent>(ctx => Play(ctx.audioEffect));
        EventBus.Subscribe<PauseAudioEvent>(ctx => Pause(ctx.audioEffect));
        EventBus.Subscribe<ResumeAudioEvent>(ctx => Resume(ctx.audioEffect));
        EventBus.Subscribe<StopAudioEvent>(ctx => Stop(ctx.audioEffect));
    }
    void OnDisable()
    {
        EventBus.UnSubscribe<PlayAudioEvent>(ctx => Play(ctx.audioEffect));
        EventBus.UnSubscribe<PauseAudioEvent>(ctx => Pause(ctx.audioEffect));
        EventBus.UnSubscribe<ResumeAudioEvent>(ctx => Resume(ctx.audioEffect));
        EventBus.UnSubscribe<StopAudioEvent>(ctx => Stop(ctx.audioEffect));
    }
    void Play(AudioEffect effect)
    {
        AudioSource audioSource = GetAudioSource();
        LoadAudioSource(effect, audioSource);
        loadedAudioSources[effect] = audioSource;

        activeAudioSources.Add(audioSource);
        audioSource.Play();
    }
    void Resume(AudioEffect effect)
    {
        AudioSource audioSource = loadedAudioSources[effect];
        audioSource.Play();
    }
    void Pause(AudioEffect effect)
    {
        AudioSource audioSource = loadedAudioSources[effect];
        audioSource.Pause();
    }
    void Stop(AudioEffect effect)
    {
        AudioSource audioSource = loadedAudioSources[effect];
        audioSource.Stop();
        ReturnSourceToPool(audioSource);
        loadedAudioSources.Remove(effect);
        activeAudioSources.Remove(audioSource);
    }

    public void Mute(bool toggle)
    {
        for (int i = 0; i < activeAudioSources.Count; i++)
        {
            activeAudioSources[i].mute = toggle;
        }
    }

    private AudioSource GetAudioSource()
    {
        AudioSource source = sourcePool.Count < 1
        ? CreateAudioObject()
        : sourcePool.Dequeue();

        source.gameObject.SetActive(true);

        return source;
    }
    private void ReturnSourceToPool(AudioSource source)
    {
        sourcePool.Enqueue(source);
        ClearAudioSource(source);
        source.gameObject.SetActive(false);
    }

    #region Audio Manager Helper Methods
    private AudioSource CreateAudioObject()
    {
        GameObject audioObject = new GameObject("Audio Object");
        audioObject.transform.SetParent(transform);
        AudioSource source = audioObject.AddComponent<AudioSource>();

        return source;
    }
    private void LoadAudioSource(AudioEffect effect, AudioSource audioSource)
    {
        audioSource.clip = effect.Clip;
        audioSource.mute = effect.Mute;
        audioSource.loop = effect.Loop;
        audioSource.playOnAwake = effect.PlayOnAwake;
        audioSource.volume = effect.Volume;
        audioSource.pitch = effect.Pitch;
    }

    private void ClearAudioSource(AudioSource source)
    {
        source.clip = null;
        source.mute = false;
        source.loop = false;
        source.playOnAwake = false;
        source.pitch = 1f;
        source.volume = 1f;
    }
    #endregion
}