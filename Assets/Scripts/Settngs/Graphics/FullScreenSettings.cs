using System;
using UnityEngine;

public class FullScreenSettings : BoolListItem
{
    [SerializeField] private GraphicsData graphicsData;

    public static event Action<bool> OnFullScreenChanged;

    protected override void OnEnable()
    {
        base.OnEnable();
        currentValue = FullScreenGraphicsSettings.GetFullScreen(graphicsData);
        UpdateText();
        UpdateButtonStates();
    }

    protected override void OnBoolValueChanged()
    {
        FullScreenGraphicsSettings.SetFullScreen(graphicsData, currentValue);
        OnFullScreenChanged?.Invoke(currentValue);
    }
}
