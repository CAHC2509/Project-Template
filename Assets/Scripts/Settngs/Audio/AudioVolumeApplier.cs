using UnityEngine;

public class AudioVolumeApplier
{
    private readonly AudioData audioData;

    public AudioVolumeApplier(AudioData audioData)
    {
        this.audioData = audioData;
    }

    public void ApplyAllVolumes()
    {
        float mainVolume = AudioVolumeSettings.GetMainVolume(audioData);
        ApplyVolume(audioData.MainVolumeKey, mainVolume);

        float musicVolume = AudioVolumeSettings.GetMusicVolume(audioData);
        ApplyVolume(audioData.MusicVolumeKey, musicVolume);

        float sfxVolume = AudioVolumeSettings.GetSFXVolume(audioData);
        ApplyVolume(audioData.SFXVolumeKey, sfxVolume);
    }

    public void ApplyVolume(string parameterName, float value)
    {
        float dB = value > 0 ? Mathf.Log10(value) * 20f : -80f;
        audioData.MainMixer.SetFloat(parameterName, dB);
    }
}
