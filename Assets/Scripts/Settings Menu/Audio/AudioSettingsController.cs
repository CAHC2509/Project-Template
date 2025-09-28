using UnityEngine;
using UnityEngine.Audio;

public class AudioSettingsController : ControllerBase, IAudioSettingsController
{
    [SerializeField] private AudioMixer mixer;

    private ISaveSystem saveSystem;
    private IAudioSettingsView view;
    private AudioSettingsEntity audioSettings;

    private void Awake()
    {
        view = GetComponentInChildren<IAudioSettingsView>();
    }

    public void Dependencies(ISaveSystem saveSystem)
    {
        this.saveSystem = saveSystem;
    }

    public override void Initialize()
    {
        base.Initialize();

        LoadSettings();
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
        if (saveSystem.HasKey(Constants.AUDIO_SETTINGS_KEY))
            audioSettings = (AudioSettingsEntity)saveSystem.Load(Constants.AUDIO_SETTINGS_KEY, typeof(AudioSettingsEntity));
        else
            audioSettings = new AudioSettingsEntity(1f, 1f, 1f, 1f);
    }

    public void InitializeSettings()
    {
        mixer.SetFloat(Constants.GENERAL_VOLUME_KEY, ConvertToDecibels(audioSettings.GeneralVolume));
        mixer.SetFloat(Constants.MUSIC_VOLUME_KEY, ConvertToDecibels(audioSettings.MusicVolume));
        mixer.SetFloat(Constants.EFFECTS_VOLUME_KEY, ConvertToDecibels(audioSettings.EffectsVolume));
        mixer.SetFloat(Constants.UI_VOLUME_KEY, ConvertToDecibels(audioSettings.UIVolume));
    }

    public void SaveSettings()
    {
        saveSystem.Save(Constants.AUDIO_SETTINGS_KEY, audioSettings);
        audioSettings.CleanPendingChanges();
    }

    public void SetDefaultSettings()
    {
        audioSettings = new AudioSettingsEntity(1f, 1f, 1f, 1f);
        UpdateGeneralVolume(audioSettings.GeneralVolume);
        UpdateMusicVolume(audioSettings.MusicVolume);
        UpdateEffectsVolume(audioSettings.EffectsVolume);
        UpdateUIVolume(audioSettings.UIVolume);
        view.SetSliders();

        SaveSettings();
    }

    public void UpdateGeneralVolume(float volume)
    {
        audioSettings.SetGeneralVolume(volume);
        mixer.SetFloat(Constants.GENERAL_VOLUME_KEY, ConvertToDecibels(volume));
        view.UpdateGeneralVolumeText();
    }

    public void UpdateMusicVolume(float volume)
    {
        audioSettings.SetMusicVolume(volume);
        mixer.SetFloat(Constants.MUSIC_VOLUME_KEY, ConvertToDecibels(volume));
        view.UpdateMusicVolumeText();
    }

    public void UpdateEffectsVolume(float volume)
    {
        audioSettings.SetEffectsVolume(volume);
        mixer.SetFloat(Constants.EFFECTS_VOLUME_KEY, ConvertToDecibels(volume));
        view.UpdateEffectsVolumeText();
    }

    public void UpdateUIVolume(float volume)
    {
        audioSettings.SetUIVolume(volume);
        mixer.SetFloat(Constants.UI_VOLUME_KEY, ConvertToDecibels(volume));
        view.UpdateUIVolumeText();
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