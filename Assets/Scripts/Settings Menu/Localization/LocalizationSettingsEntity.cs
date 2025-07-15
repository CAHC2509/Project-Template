using System;
using UnityEngine;

public class LocalizationSettingsEntity
{
    public string CurrentLanguageCode { get; private set; }

    public event Action OnNewLanguageSelected;

    public LocalizationSettingsEntity(string language)
    {
        CurrentLanguageCode = language;
    }

    public void SelectNewLanguage(string language)
    {
        CurrentLanguageCode = language;
        OnNewLanguageSelected?.Invoke();
    }
}
