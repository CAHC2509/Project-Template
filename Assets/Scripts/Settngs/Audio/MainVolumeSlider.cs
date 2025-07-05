using System;
using UnityEngine;

public class MainVolumeSlider : SelectableSliderItem
{
    [SerializeField] private AudioData audioData;

    public static event Action<float> OnVolumeChanged;

    protected override void OnEnable()
    {
        base.OnEnable();
        slider.value = AudioVolumeSettings.GetMainVolume(audioData);
    }

    protected override void OnSliderChanged(float value)
    {
        base.OnSliderChanged(value);

        OnVolumeChanged?.Invoke(value);
        AutoSelect();
    }
}
