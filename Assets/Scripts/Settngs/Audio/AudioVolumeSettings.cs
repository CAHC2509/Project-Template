using UnityEngine;

public static class AudioVolumeSettings
{
    public static float GetMainVolume(AudioData data) => PlayerPrefs.GetFloat(data.MainVolumeKey, 1f);
    public static float GetMusicVolume(AudioData data) => PlayerPrefs.GetFloat(data.MusicVolumeKey, 1f);
    public static float GetSFXVolume(AudioData data) => PlayerPrefs.GetFloat(data.SFXVolumeKey, 1f);

    public static void SetMainVolume(AudioData data, float value) => PlayerPrefs.SetFloat(data.MainVolumeKey, value);
    public static void SetMusicVolume(AudioData data, float value) => PlayerPrefs.SetFloat(data.MusicVolumeKey, value);
    public static void SetSFXVolume(AudioData data, float value) => PlayerPrefs.SetFloat(data.SFXVolumeKey, value);

    public static void Save() => PlayerPrefs.Save();
}
