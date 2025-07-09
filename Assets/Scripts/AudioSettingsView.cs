using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioSettingsView : MonoBehaviour
{
    [Header("Sliders")]
    [SerializeField] private Slider generalVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider effectsVolumeSlider;

    [Space, Header("Texts")]
    [SerializeField] private TextMeshProUGUI generalVolumeText;
    [SerializeField] private TextMeshProUGUI musicVolumeText;
    [SerializeField] private TextMeshProUGUI effectsVolumeText;

    private AudioSettingsEntity audioSettings;

    public void Initialize(AudioSettingsEntity audioSettings)
    {
        this.audioSettings = audioSettings;

        AddListeners();
        SetSliders();
        SetTexts();
    }

    private void AddListeners()
    {
        generalVolumeSlider.onValueChanged.AddListener(GeneralVolumeChanged);
        musicVolumeSlider.onValueChanged.AddListener(MusicVolumeChanged);
        effectsVolumeSlider.onValueChanged.AddListener(EffectsVolumeChanged);
    }

    private void SetSliders()
    {
        generalVolumeSlider.value = audioSettings.GeneralVolume;
        musicVolumeSlider.value = audioSettings.MusicVolume;
        effectsVolumeSlider.value = audioSettings.EffectsVolume;
    }

    private void SetTexts()
    {
        generalVolumeText.text = VolumeToPercentage(audioSettings.GeneralVolume);
        musicVolumeText.text = VolumeToPercentage(audioSettings.MusicVolume);
        effectsVolumeText.text = VolumeToPercentage(audioSettings.EffectsVolume);
    }

    private void GeneralVolumeChanged(float volume)
    {
        audioSettings.SetGeneralVolume(volume);
        generalVolumeText.text = VolumeToPercentage(volume);
    }

    private void MusicVolumeChanged(float volume)
    {
        audioSettings.SetMusicVolume(volume);
        musicVolumeText.text = VolumeToPercentage(volume);
    }

    private void EffectsVolumeChanged(float volume)
    {
        audioSettings.SetEffectsVolume(volume);
        effectsVolumeText.text = VolumeToPercentage(volume);
    }

    private string VolumeToPercentage(float volume) => ((int)(volume * 100f)).ToString();
}