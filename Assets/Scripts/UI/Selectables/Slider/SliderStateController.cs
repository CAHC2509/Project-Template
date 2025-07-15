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

    private void Awake()
    {
        InitializeStateMachine();
        AddListeners();
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
}
