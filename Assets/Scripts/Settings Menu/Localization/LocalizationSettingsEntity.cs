using System;
using UnityEngine;

[Serializable]
public class LocalizationSettingsEntity
{
    [SerializeField] private string currentLanguageCode;

    public string CurrentLanguageCode => currentLanguageCode;

    public LocalizationSettingsEntity(string language)
    {
        currentLanguageCode = language;
    }

    public void SetCurrentLanguage(string language)
    {
        currentLanguageCode = language;
    }
}
