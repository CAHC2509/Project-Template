using UnityEngine;
using UnityEngine.Audio;

public class AudioSettingsController : ControllerBase, IAudioSettingsController
{
    [SerializeField] private AudioMixer mixer;

    private const string GENERAL_VOLUME_KEY = "GeneralVolume";
    private const string MUSIC_VOLUME_KEY = "MusicVolume";
    private const string EFFECTS_VOLUME_KEY = "EffectsVolume";

    private IAudioSettingsView view;
    private AudioSettingsEntity audioSettings;

    private void Awake()
    {
        view = GetComponentInChildren<IAudioSettingsView>();
        LoadSettings();
    }

    public override void Initialize()
    {
        base.Initialize();

        InitializeSettings();
        view.Initialize();
    }

    public override void Conclude()
    {
        base.Conclude();

        view.Conclude();
    }

    public void LoadSettings()
    {
        float general = PlayerPrefs.GetFloat(GENERAL_VOLUME_KEY, 1f);
        float music = PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY, 1f);
        float effects = PlayerPrefs.GetFloat(EFFECTS_VOLUME_KEY, 1f);

        audioSettings = new AudioSettingsEntity(general, music, effects);
    }

    public void InitializeSettings()
    {
        mixer.SetFloat(GENERAL_VOLUME_KEY, ConvertToDecibels(audioSettings.GeneralVolume));
        mixer.SetFloat(MUSIC_VOLUME_KEY, ConvertToDecibels(audioSettings.MusicVolume));
        mixer.SetFloat(EFFECTS_VOLUME_KEY, ConvertToDecibels(audioSettings.EffectsVolume));
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat(GENERAL_VOLUME_KEY, audioSettings.GeneralVolume);
        PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, audioSettings.MusicVolume);
        PlayerPrefs.SetFloat(EFFECTS_VOLUME_KEY, audioSettings.EffectsVolume);
        PlayerPrefs.Save();
    }

    public void UpdateGeneralVolume(float volume)
    {
        audioSettings.SetGeneralVolume(volume);

        mixer.SetFloat(GENERAL_VOLUME_KEY, ConvertToDecibels(volume));
        PlayerPrefs.SetFloat(GENERAL_VOLUME_KEY, volume);
        PlayerPrefs.Save();

        view.UpdateGeneralVolumeText();
    }

    public void UpdateMusicVolume(float volume)
    {
        audioSettings.SetMusicVolume(volume);

        mixer.SetFloat(MUSIC_VOLUME_KEY, ConvertToDecibels(volume));
        PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, volume);
        PlayerPrefs.Save();

        view.UpdateMusicVolumeText();
    }

    public void UpdateEffectsVolume(float volume)
    {
        audioSettings.SetEffectsVolume(volume);

        mixer.SetFloat(EFFECTS_VOLUME_KEY, ConvertToDecibels(volume));
        PlayerPrefs.SetFloat(EFFECTS_VOLUME_KEY, volume);
        PlayerPrefs.Save();

        view.UpdateEffectsVolumeText();
    }

    private float ConvertToDecibels(float linearVolume)
    {
        if (linearVolume <= 0.0001f)
            return -80f;

        return 20f * Mathf.Log10(linearVolume);
    }

    public string VolumeToPercentage(float volume)
    {
        return ((int)(volume * 100f)).ToString();
    }

    public AudioSettingsEntity GetModel()
    {
        return audioSettings;
    }
}