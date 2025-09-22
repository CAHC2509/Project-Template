using UnityEngine;
using UnityEngine.Audio;

public class AudioSettingsController : ControllerBase, IAudioSettingsController
{
    [SerializeField] private AudioMixer mixer;

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
        float general = PlayerPrefs.GetFloat(Constants.GENERAL_VOLUME_KEY, 1f);
        float music = PlayerPrefs.GetFloat(Constants.MUSIC_VOLUME_KEY, 1f);
        float effects = PlayerPrefs.GetFloat(Constants.EFFECTS_VOLUME_KEY, 1f);

        audioSettings = new AudioSettingsEntity(general, music, effects);
    }

    public void InitializeSettings()
    {
        mixer.SetFloat(Constants.GENERAL_VOLUME_KEY, ConvertToDecibels(audioSettings.GeneralVolume));
        mixer.SetFloat(Constants.MUSIC_VOLUME_KEY, ConvertToDecibels(audioSettings.MusicVolume));
        mixer.SetFloat(Constants.EFFECTS_VOLUME_KEY, ConvertToDecibels(audioSettings.EffectsVolume));
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat(Constants.GENERAL_VOLUME_KEY, audioSettings.GeneralVolume);
        PlayerPrefs.SetFloat(Constants.MUSIC_VOLUME_KEY, audioSettings.MusicVolume);
        PlayerPrefs.SetFloat(Constants.EFFECTS_VOLUME_KEY, audioSettings.EffectsVolume);
        PlayerPrefs.Save();
    }

    public void UpdateGeneralVolume(float volume)
    {
        audioSettings.SetGeneralVolume(volume);

        mixer.SetFloat(Constants.GENERAL_VOLUME_KEY, ConvertToDecibels(volume));
        PlayerPrefs.SetFloat(Constants.GENERAL_VOLUME_KEY, volume);
        PlayerPrefs.Save();

        view.UpdateGeneralVolumeText();
    }

    public void UpdateMusicVolume(float volume)
    {
        audioSettings.SetMusicVolume(volume);

        mixer.SetFloat(Constants.MUSIC_VOLUME_KEY, ConvertToDecibels(volume));
        PlayerPrefs.SetFloat(Constants.MUSIC_VOLUME_KEY, volume);
        PlayerPrefs.Save();

        view.UpdateMusicVolumeText();
    }

    public void UpdateEffectsVolume(float volume)
    {
        audioSettings.SetEffectsVolume(volume);

        mixer.SetFloat(Constants.EFFECTS_VOLUME_KEY, ConvertToDecibels(volume));
        PlayerPrefs.SetFloat(Constants.EFFECTS_VOLUME_KEY, volume);
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

    public void EnableView()
    {
        view.EnableView();
    }

    public void DisableView()
    {
        view.DisableView();
    }
}