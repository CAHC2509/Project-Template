using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class SelectableSliderItem : SelectableButton
{
    [Header("List components")]
    [SerializeField] protected Slider slider;
    [SerializeField] protected TextMeshProUGUI itemText;

    private const float VOLUME_CONSTANT = 0.01f;

    public static event Action OnSliderModified;

    private void Start() => UpdateText(slider.value);

    protected override void OnEnable()
    {
        base.OnEnable();

        slider.onValueChanged.AddListener(OnSliderChanged);
        slider.onValueChanged.AddListener(UpdateText);

        UIInputHandler.OnLeft += DecreaseValue;
        UIInputHandler.OnRight += IncreaseValue;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        slider.onValueChanged.RemoveListener(OnSliderChanged);
        slider.onValueChanged.RemoveListener(UpdateText);

        UIInputHandler.OnLeft -= DecreaseValue;
        UIInputHandler.OnRight -= IncreaseValue;
    }

    private void IncreaseValue()
    {
        if (!isSelected) return;
        if (slider.value >= 1f) return;

        slider.value += VOLUME_CONSTANT;
    }

    private void DecreaseValue()
    {
        if (!isSelected) return;
        if (slider.value <= 0f) return;

        slider.value -= VOLUME_CONSTANT;
    }

    protected virtual void UpdateText(float value)
    {
        int newValue = (int)(value * 101f);
        newValue = Mathf.Clamp(newValue, 0, 100);
        itemText.text = newValue.ToString();
    }

    protected virtual void OnSliderChanged(float value)
    {
        OnSliderModified?.Invoke();
        AutoSelect();
    }

    protected virtual void SetSliderValue(float value) => slider.value = value;
}
