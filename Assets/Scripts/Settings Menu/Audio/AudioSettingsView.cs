using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioSettingsView : ViewBase, IAudioSettingsView
{
    [Header("Sliders")]
    [SerializeField] private Slider generalVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider effectsVolumeSlider;
    [SerializeField] private Slider uiVolumeSlider;

    [Header("Slider controllers")]
    [SerializeField] private SliderStateController generalVolumeController;
    [SerializeField] private SliderStateController musicVolumeController;
    [SerializeField] private SliderStateController effectsVolumeController;
    [SerializeField] private SliderStateController uiVolumeController;

    [Space, Header("Texts")]
    [SerializeField] private TextMeshProUGUI generalVolumeText;
    [SerializeField] private TextMeshProUGUI musicVolumeText;
    [SerializeField] private TextMeshProUGUI effectsVolumeText;
    [SerializeField] private TextMeshProUGUI uiVolumeText;

    private IAudioSettingsController controller;

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
        UpdateUIVolumeText();
    }

    protected override void AddPersistentListeners()
    {
        generalVolumeSlider.onValueChanged.AddListener(GeneralVolumeChanged);
        musicVolumeSlider.onValueChanged.AddListener(MusicVolumeChanged);
        effectsVolumeSlider.onValueChanged.AddListener(EffectsVolumeChanged);
        uiVolumeSlider.onValueChanged.AddListener(UIVolumeChanged);

        generalVolumeController.OnValueIncreaseRequest += IncreaseGeneralVolume;
        generalVolumeController.OnValueDecreaseRequest += DecreaseGeneralVolume;

        musicVolumeController.OnValueIncreaseRequest += IncreaseMusicVolume;
        musicVolumeController.OnValueDecreaseRequest += DecreaseMusicVolume;

        effectsVolumeController.OnValueIncreaseRequest += IncreaseEffectsVolume;
        effectsVolumeController.OnValueDecreaseRequest += DecreaseEffectsVolume;

        uiVolumeController.OnValueIncreaseRequest += IncreaseUIVolume;
        uiVolumeController.OnValueDecreaseRequest += DecreaseUIVolume;
    }

    protected override void RemovePersistentListeners()
    {
        generalVolumeSlider.onValueChanged.RemoveListener(GeneralVolumeChanged);
        musicVolumeSlider.onValueChanged.RemoveListener(MusicVolumeChanged);
        effectsVolumeSlider.onValueChanged.RemoveListener(EffectsVolumeChanged);
        uiVolumeSlider.onValueChanged.RemoveListener(UIVolumeChanged);

        generalVolumeController.OnValueIncreaseRequest -= IncreaseGeneralVolume;
        generalVolumeController.OnValueDecreaseRequest -= DecreaseGeneralVolume;

        musicVolumeController.OnValueIncreaseRequest -= IncreaseMusicVolume;
        musicVolumeController.OnValueDecreaseRequest -= DecreaseMusicVolume;

        effectsVolumeController.OnValueIncreaseRequest -= IncreaseEffectsVolume;
        effectsVolumeController.OnValueDecreaseRequest -= DecreaseEffectsVolume;

        uiVolumeController.OnValueIncreaseRequest -= IncreaseUIVolume;
        uiVolumeController.OnValueDecreaseRequest -= DecreaseUIVolume;
    }

    private void SetSliders()
    {
        generalVolumeSlider.SetValueWithoutNotify(controller.GetModel().GeneralVolume);
        musicVolumeSlider.SetValueWithoutNotify(controller.GetModel().MusicVolume);
        effectsVolumeSlider.SetValueWithoutNotify(controller.GetModel().EffectsVolume);
        uiVolumeSlider.SetValueWithoutNotify(controller.GetModel().UIVolume);
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

    private void UIVolumeChanged(float volume)
    {
        controller.UpdateUIVolume(volume);
    }

    public void UpdateGeneralVolumeText() => generalVolumeText.text = controller.VolumeToPercentage(controller.GetModel().GeneralVolume);
    public void UpdateMusicVolumeText() => musicVolumeText.text = controller.VolumeToPercentage(controller.GetModel().MusicVolume);
    public void UpdateEffectsVolumeText() => effectsVolumeText.text = controller.VolumeToPercentage(controller.GetModel().EffectsVolume);
    public void UpdateUIVolumeText() => uiVolumeText.text = controller.VolumeToPercentage(controller.GetModel().UIVolume);

    private void ChangeSlider(Slider slider, float delta)
    {
        float newValue = Mathf.Clamp01(slider.value + delta);
        slider.value = newValue;
    }

    private void IncreaseGeneralVolume() => ChangeSlider(generalVolumeSlider, Constants.VOLUME_CONSTANT);
    private void DecreaseGeneralVolume() => ChangeSlider(generalVolumeSlider, -Constants.VOLUME_CONSTANT);
    private void IncreaseMusicVolume() => ChangeSlider(musicVolumeSlider, Constants.VOLUME_CONSTANT);
    private void DecreaseMusicVolume() => ChangeSlider(musicVolumeSlider, -Constants.VOLUME_CONSTANT);
    private void IncreaseEffectsVolume() => ChangeSlider(effectsVolumeSlider, Constants.VOLUME_CONSTANT);
    private void DecreaseEffectsVolume() => ChangeSlider(effectsVolumeSlider, -Constants.VOLUME_CONSTANT);
    private void IncreaseUIVolume() => ChangeSlider(uiVolumeSlider, Constants.VOLUME_CONSTANT);
    private void DecreaseUIVolume() => ChangeSlider(uiVolumeSlider, -Constants.VOLUME_CONSTANT);
}
