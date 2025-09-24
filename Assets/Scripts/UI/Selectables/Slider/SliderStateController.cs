using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderStateController : SelectableStateController
{
    [SerializeField] private Image[] mainImages;
    [SerializeField] private Image[] secondaryImages;
    [SerializeField] private TextMeshProUGUI[] texts;
    [SerializeField] private AudioClip sliderModifiedSFX;

    public event Action OnValueIncreaseRequest;
    public event Action OnValueDecreaseRequest;

    private Slider slider;

    private void Awake()
    {
        slider = GetComponentInChildren<Slider>();
        DisableSliderNavigation();
        InitializeStateMachine();
    }

    private void InitializeStateMachine()
    {
        normalState = new SliderNormalState(selectableData, mainImages, secondaryImages, texts);
        selectedState = new SliderSelectedState(selectableData, mainImages, secondaryImages, texts);
        Initialize(normalState);
    }

    protected override void AddTemporalListeners()
    {
        base.AddTemporalListeners();

        UIInputManager.OnRight += IncreaseSliderValue;
        UIInputManager.OnLeft += DecreaseSliderValue;
    }

    protected override void RemoveTemporalListeners()
    {
        base.RemoveTemporalListeners();

        UIInputManager.OnRight -= IncreaseSliderValue;
        UIInputManager.OnLeft -= DecreaseSliderValue;
    }

    private void IncreaseSliderValue()
    {
        if (currentState != selectedState) return;
        OnValueIncreaseRequest?.Invoke();
        AudioManager.Instance.PlaySFX(sliderModifiedSFX);
    }

    private void DecreaseSliderValue()
    {
        if (currentState != selectedState) return;
        OnValueDecreaseRequest?.Invoke();
        AudioManager.Instance.PlaySFX(sliderModifiedSFX);
    }

    private void DisableSliderNavigation()
    {
        var navigation = slider.navigation;
        navigation.mode = Navigation.Mode.None;
        slider.navigation = navigation;
    }
}
