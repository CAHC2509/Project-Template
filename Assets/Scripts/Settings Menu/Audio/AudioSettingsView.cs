using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioSettingsView : ViewBase, IAudioSettingsView
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

    private IAudioSettingsController controller;
    private const float VOLUME_CONSTANT = 0.01f;

    private void Awake()
    {
        controller = GetComponentInParent<IAudioSettingsController>();
        defaultSelection = generalVolumeController.Button;
    }

    public override void Initialize()
    {
        base.Initialize();

        SetSliders();
        UpdateGeneralVolumeText();
        UpdateMusicVolumeText();
        UpdateEffectsVolumeText();
    }

    protected override void AddPersistentListeners()
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

    protected override void RemovePersistentListeners()
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
        generalVolumeSlider.SetValueWithoutNotify(controller.GetModel().GeneralVolume);
        musicVolumeSlider.SetValueWithoutNotify(controller.GetModel().MusicVolume);
        effectsVolumeSlider.SetValueWithoutNotify(controller.GetModel().EffectsVolume);
    }

    private void GeneralVolumeChanged(float volume)
    {
        controller.UpdateGeneralVolume(volume);
    }

    private void MusicVolumeChanged(float volume)
    {
        controller.UpdateMusicVolume(volume);
    }

    private void EffectsVolumeChanged(float volume)
    {
        controller.UpdateEffectsVolume(volume);
    }

    public void UpdateGeneralVolumeText()
    {
        generalVolumeText.text = controller.VolumeToPercentage(controller.GetModel().GeneralVolume);
    }

    public void UpdateMusicVolumeText()
    {
        musicVolumeText.text = controller.VolumeToPercentage(controller.GetModel().MusicVolume);
    }

    public void UpdateEffectsVolumeText()
    {
        effectsVolumeText.text = controller.VolumeToPercentage(controller.GetModel().EffectsVolume);
    }

    private void ChangeSlider(Slider slider, float delta)
    {
        float newValue = Mathf.Clamp01(slider.value + delta);
        slider.value = newValue;
    }

    private void IncreaseGeneralVolume() => ChangeSlider(generalVolumeSlider, VOLUME_CONSTANT);
    private void DecreaseGeneralVolume() => ChangeSlider(generalVolumeSlider, -VOLUME_CONSTANT);
    private void IncreaseMusicVolume() => ChangeSlider(musicVolumeSlider, VOLUME_CONSTANT);
    private void DecreaseMusicVolume() => ChangeSlider(musicVolumeSlider, -VOLUME_CONSTANT);
    private void IncreaseEffectsVolume() => ChangeSlider(effectsVolumeSlider, VOLUME_CONSTANT);
    private void DecreaseEffectsVolume() => ChangeSlider(effectsVolumeSlider, -VOLUME_CONSTANT);
}
