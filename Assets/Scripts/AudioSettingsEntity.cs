using System;
using UnityEngine;

[System.Serializable]
public class AudioSettingsEntity
{
    public float GeneralVolume { get; private set; }
    public float MusicVolume { get; private set; }
    public float EffectsVolume { get; private set; }

    public event Action<float> OnGeneralVolumeChanged;
    public event Action<float> OnMusicVolumeChanged;
    public event Action<float> OnEffectsVolumeChanged;

    public AudioSettingsEntity(float generalVolume, float musicVolume, float effectsVolume)
    {
        GeneralVolume = Mathf.Clamp01(generalVolume);
        MusicVolume = Mathf.Clamp01(musicVolume);
        EffectsVolume = Mathf.Clamp01(effectsVolume);
    }

    public void SetGeneralVolume(float newVolume)
    {
        GeneralVolume = Mathf.Clamp01(newVolume);
        OnGeneralVolumeChanged?.Invoke(GeneralVolume);
    }

    public void SetMusicVolume(float newVolume)
    {
        MusicVolume = Mathf.Clamp01(newVolume);
        OnMusicVolumeChanged?.Invoke(MusicVolume);
    }

    public void SetEffectsVolume(float newVolume)
    {
        EffectsVolume = Mathf.Clamp01(newVolume);
        OnEffectsVolumeChanged?.Invoke(EffectsVolume);
    }
}
