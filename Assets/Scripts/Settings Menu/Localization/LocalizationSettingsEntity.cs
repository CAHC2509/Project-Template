using System;

[Serializable]
public class LocalizationSettingsEntity
{
    public string CurrentLanguageCode { get; private set; }

    public LocalizationSettingsEntity(string language)
    {
        SetCurrentLanguage(language);
    }

    public void SetCurrentLanguage(string language)
    {
        CurrentLanguageCode = language;
    }
}
