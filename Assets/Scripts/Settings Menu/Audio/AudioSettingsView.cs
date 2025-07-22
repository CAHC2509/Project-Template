using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioSettingsView : MonoBehaviour
{
    [Header("Sliders")]
    [SerializeField] private Slider generalVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider effectsVolumeSlider;

    [Header("Slider controllers")]
    [SerializeField] private SliderStateController generalVolumeController;
    [SerializeField] private SliderStateController musicVolumeController;
    [SerializeField] private SliderStateController effectsVolumeController;

    [Space, Header("Texts")]
    [SerializeField] private TextMeshProUGUI generalVolumeText;
    [SerializeField] private TextMeshProUGUI musicVolumeText;
    [SerializeField] private TextMeshProUGUI effectsVolumeText;

    private const float VOLUME_CONSTANT = 0.01f;
    private AudioSettingsEntity audioSettings;

    public void Initialize(AudioSettingsEntity audioSettings)
    {
        this.audioSettings = audioSettings;

        AddListeners();
        SetSliders();
        SetTexts();
    }

    public void Conclude()
    {
        RemoveListeners();
    }

    private void AddListeners()
    {
        generalVolumeSlider.onValueChanged.AddListener(GeneralVolumeChanged);
        musicVolumeSlider.onValueChanged.AddListener(MusicVolumeChanged);
        effectsVolumeSlider.onValueChanged.AddListener(EffectsVolumeChanged);

        generalVolumeController.OnValueIncreaseRequest += IncreaseGeneralVolume;
        generalVolumeController.OnValueDecreaseRequest += DecreaseGeneralVolume;

        musicVolumeController.OnValueIncreaseRequest += IncreaseMusicVolume;
        musicVolumeController.OnValueDecreaseRequest += DecreaseMusicVolume;

        effectsVolumeController.OnValueIncreaseRequest += IncreaseEffectsVolume;
        effectsVolumeController.OnValueDecreaseRequest += DecreaseEffectsVolume;
    }
    
    private void RemoveListeners()
    {
        generalVolumeSlider.onValueChanged.RemoveListener(GeneralVolumeChanged);
        musicVolumeSlider.onValueChanged.RemoveListener(MusicVolumeChanged);
        effectsVolumeSlider.onValueChanged.RemoveListener(EffectsVolumeChanged);

        generalVolumeController.OnValueIncreaseRequest -= IncreaseGeneralVolume;
        generalVolumeController.OnValueDecreaseRequest -= DecreaseGeneralVolume;

        musicVolumeController.OnValueIncreaseRequest -= IncreaseMusicVolume;
        musicVolumeController.OnValueDecreaseRequest -= DecreaseMusicVolume;

        effectsVolumeController.OnValueIncreaseRequest -= IncreaseEffectsVolume;
        effectsVolumeController.OnValueDecreaseRequest -= DecreaseEffectsVolume;
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

    private void IncreaseGeneralVolume() => GeneralVolumeChanged(generalVolumeSlider.value += VOLUME_CONSTANT);
    private void DecreaseGeneralVolume() => GeneralVolumeChanged(generalVolumeSlider.value -= VOLUME_CONSTANT);
    private void IncreaseMusicVolume() => GeneralVolumeChanged(musicVolumeSlider.value += VOLUME_CONSTANT);
    private void DecreaseMusicVolume() => GeneralVolumeChanged(musicVolumeSlider.value -= VOLUME_CONSTANT);
    private void IncreaseEffectsVolume() => GeneralVolumeChanged(effectsVolumeSlider.value += VOLUME_CONSTANT);
    private void DecreaseEffectsVolume() => GeneralVolumeChanged(effectsVolumeSlider.value -= VOLUME_CONSTANT);

    private string VolumeToPercentage(float volume) => ((int)(volume * 100f)).ToString();
}