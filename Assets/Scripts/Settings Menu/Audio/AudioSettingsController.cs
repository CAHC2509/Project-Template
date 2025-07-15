using UnityEngine;
using UnityEngine.Audio;

public class AudioSettingsController : MonoBehaviour, ISettings
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private AudioSettingsView view;

    private const string GENERAL_VOLUME_KEY = "GeneralVolume";
    private const string MUSIC_VOLUME_KEY = "MusicVolume";
    private const string EFFECTS_VOLUME_KEY = "EffectsVolume";
    private AudioSettingsEntity audioSettings;

    private void Awake() => GameManager.OnInitialization += OnInitialization;

    private void OnInitialization()
    {
        LoadSettings();
        AddListeners();
        InitializeSettings();
        view.Initialize(audioSettings);
    }

    private void AddListeners()
    {
        audioSettings.OnGeneralVolumeChanged += UpdateGeneralVolume;
        audioSettings.OnMusicVolumeChanged += UpdateMusicVolume;
        audioSettings.OnEffectsVolumeChanged += UpdateEffectsVolume;
    }

    public void InitializeSettings()
    {
        mixer.SetFloat(GENERAL_VOLUME_KEY, ConvertToDecibels(audioSettings.GeneralVolume));
        mixer.SetFloat(MUSIC_VOLUME_KEY, ConvertToDecibels(audioSettings.MusicVolume));
        mixer.SetFloat(EFFECTS_VOLUME_KEY, ConvertToDecibels(audioSettings.EffectsVolume));
    }

    public void LoadSettings()
    {
        float general = PlayerPrefs.GetFloat(GENERAL_VOLUME_KEY, 1f);
        float music = PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY, 1f);
        float effects = PlayerPrefs.GetFloat(EFFECTS_VOLUME_KEY, 1f);

        audioSettings = new AudioSettingsEntity(general, music, effects);
    }

    public void SaveSettings() 
    {
        PlayerPrefs.SetFloat(GENERAL_VOLUME_KEY, audioSettings.GeneralVolume);
        PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, audioSettings.MusicVolume);
        PlayerPrefs.SetFloat(EFFECTS_VOLUME_KEY, audioSettings.EffectsVolume);
        PlayerPrefs.Save();
    }

    private void UpdateGeneralVolume(float volume)
    {
        mixer.SetFloat(GENERAL_VOLUME_KEY, ConvertToDecibels(volume));
        PlayerPrefs.SetFloat(GENERAL_VOLUME_KEY, volume);
        PlayerPrefs.Save();
    }

    private void UpdateMusicVolume(float volume)
    {
        mixer.SetFloat(MUSIC_VOLUME_KEY, ConvertToDecibels(volume));
        PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, volume);
        PlayerPrefs.Save();
    }

    private void UpdateEffectsVolume(float volume)
    {
        mixer.SetFloat(EFFECTS_VOLUME_KEY, ConvertToDecibels(volume));
        PlayerPrefs.SetFloat(EFFECTS_VOLUME_KEY, volume);
        PlayerPrefs.Save();
    }

    private float ConvertToDecibels(float linearVolume)
    {
        if (linearVolume <= 0.0001f)
            return -80f;

        return 20f * Mathf.Log10(linearVolume);
    }
}
