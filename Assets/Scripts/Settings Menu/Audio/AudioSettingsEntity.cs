using System;
using UnityEngine;

[Serializable]
public class AudioSettingsEntity
{
    [SerializeField] private float generalVolume;
    [SerializeField] private float musicVolume;
    [SerializeField] private float effectsVolume;
    [SerializeField] private float uiVolume;

    private bool pendingChanges;

    public float GeneralVolume => generalVolume;
    public float MusicVolume => musicVolume;
    public float EffectsVolume => effectsVolume;
    public float UIVolume => uiVolume;
    public bool PendingChanges => pendingChanges;

    public AudioSettingsEntity(float generalVolume, float musicVolume, float effectsVolume, float uiVolume)
    {
        this.generalVolume = Mathf.Clamp01(generalVolume);
        this.musicVolume = Mathf.Clamp01(musicVolume);
        this.effectsVolume = Mathf.Clamp01(effectsVolume);
        this.uiVolume = Mathf.Clamp01(uiVolume);

        pendingChanges = false;
    }

    public void SetGeneralVolume(float newVolume)
    {
        generalVolume = Mathf.Clamp01(newVolume);
        pendingChanges = true;
    }

    public void SetMusicVolume(float newVolume)
    {
        musicVolume = Mathf.Clamp01(newVolume);
        pendingChanges = true;
    }

    public void SetEffectsVolume(float newVolume)
    {
        effectsVolume = Mathf.Clamp01(newVolume);
        pendingChanges = true;
    }

    public void SetUIVolume(float newVolume)
    {
        uiVolume = Mathf.Clamp01(newVolume);
        pendingChanges = true;
    }

    public void CleanPendingChanges()
    {
        pendingChanges = false;
    }
}
