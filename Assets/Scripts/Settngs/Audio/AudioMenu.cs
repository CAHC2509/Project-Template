using UnityEngine;
using UnityEngine.Audio;

public class AudioMenu : SettingsSubMenuBase
{
    [Space, Header("Audio settings")]
    [SerializeField] private AudioData audioData;

    private AudioVolumeApplier volumeApplier;

    protected override void OnEnable()
    {
        base.OnEnable();

        volumeApplier = new AudioVolumeApplier(audioData);
        volumeApplier.ApplyAllVolumes();

        MainVolumeSlider.OnVolumeChanged += OnMainVolumeChanged;
        MusicVolumeSlider.OnVolumeChanged += OnMusicVolumeChanged;
        SFXVolumeSlider.OnVolumeChanged += OnSFXVolumeChanged;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        MainVolumeSlider.OnVolumeChanged -= OnMainVolumeChanged;
        MusicVolumeSlider.OnVolumeChanged -= OnMusicVolumeChanged;
        SFXVolumeSlider.OnVolumeChanged -= OnSFXVolumeChanged;

        AudioVolumeSettings.Save();
    }

    private void OnMainVolumeChanged(float value)
    {
        AudioVolumeSettings.SetMainVolume(audioData, value);
        volumeApplier.ApplyVolume(audioData.MainVolumeKey, value);
    }

    private void OnMusicVolumeChanged(float value)
    {
        AudioVolumeSettings.SetMusicVolume(audioData, value);
        volumeApplier.ApplyVolume(audioData.MusicVolumeKey, value);
    }

    private void OnSFXVolumeChanged(float value)
    {
        AudioVolumeSettings.SetSFXVolume(audioData, value);
        volumeApplier.ApplyVolume(audioData.SFXVolumeKey, value);
    }
}
