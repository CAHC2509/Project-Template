using System;
using UnityEngine;

public class MusicVolumeSlider : SelectableSliderItem
{
    [SerializeField] private AudioData audioData;

    public static event Action<float> OnVolumeChanged;

    protected override void OnEnable()
    {
        base.OnEnable();
        slider.value = AudioVolumeSettings.GetMusicVolume(audioData);
    }

    protected override void OnSliderChanged(float value)
    {
        base.OnSliderChanged(value);

        OnVolumeChanged?.Invoke(value);
        AutoSelect();
    }
}
