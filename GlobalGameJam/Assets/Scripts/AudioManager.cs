using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region singleton
    public static AudioManager Instance = null;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this);
    }
    #endregion

    public AudioSource EffectsSource;
    public AudioSource MusicSource;
    public AudioSource NarratorSource;

    public float LowPitchRange = 0.95f;
    public float HighPitchRange = 1.05f;

    public void Play(AudioClip clip)
    {
        float randomPitch = Random.Range(LowPitchRange, HighPitchRange);
        EffectsSource.pitch = randomPitch;
        EffectsSource.clip = clip;
        EffectsSource.Play();
    }

    public void PlayNarrator(AudioClip clip)
    {
        NarratorSource.clip = clip;
        NarratorSource.Play();
    }

    public void PlayMusic(AudioClip clip)
    {
        MusicSource.clip = clip;
        MusicSource.Play();
    }
}
