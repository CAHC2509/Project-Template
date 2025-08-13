using System;
using UnityEngine;

[Serializable]
public class AudioSettingsEntity
{
    public float GeneralVolume { get; private set; }
    public float MusicVolume { get; private set; }
    public float EffectsVolume { get; private set; }

    public AudioSettingsEntity(float generalVolume, float musicVolume, float effectsVolume)
    {
        GeneralVolume = Mathf.Clamp01(generalVolume);
        MusicVolume = Mathf.Clamp01(musicVolume);
        EffectsVolume = Mathf.Clamp01(effectsVolume);
    }

    public void SetGeneralVolume(float newVolume)
    {
        GeneralVolume = Mathf.Clamp01(newVolume);
    }

    public void SetMusicVolume(float newVolume)
    {
        MusicVolume = Mathf.Clamp01(newVolume);
    }

    public void SetEffectsVolume(float newVolume)
    {
        EffectsVolume = Mathf.Clamp01(newVolume);
    }
}
