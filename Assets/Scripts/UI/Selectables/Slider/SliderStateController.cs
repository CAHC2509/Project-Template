using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderStateController : SelectableStateController
{
    [SerializeField] private Image[] mainImages;
    [SerializeField] private Image[] secondaryImages;
    [SerializeField] private TextMeshProUGUI[] texts;

    public event Action OnValueIncreaseRequest;
    public event Action OnValueDecreaseRequest;

    private Slider slider;

    private void Awake()
    {
        slider = GetComponentInChildren<Slider>();
        InitializeStateMachine();
        DisableSliderNavigation();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        AddListeners();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        RemoveListeners();
    }

    private void InitializeStateMachine()
    {
        normalState = new SliderNormalState(selectableData, mainImages, secondaryImages, texts);
        selectedState = new SliderSelectedState(selectableData, mainImages, secondaryImages, texts);
        Initialize(normalState);
    }

    private void AddListeners()
    {
        UIInputManager.OnRight += IncreaseSliderValue;
        UIInputManager.OnLeft += DecreaseSliderValue;
    }

    private void RemoveListeners()
    {
        UIInputManager.OnRight -= IncreaseSliderValue;
        UIInputManager.OnLeft -= DecreaseSliderValue;
    }

    private void IncreaseSliderValue()
    {
        if (currentState != selectedState) return;
        OnValueIncreaseRequest?.Invoke();
    }

    private void DecreaseSliderValue()
    {
        if (currentState != selectedState) return;
        OnValueDecreaseRequest?.Invoke();
    }

    private void DisableSliderNavigation()
    {
        var navigation = slider.navigation;
        navigation.mode = Navigation.Mode.None;
        slider.navigation = navigation;
    }
}
