using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocalizationSettingsController : MonoBehaviour, ISettings
{
    [SerializeField] private LocalizationSettingsView view;

    private const string LANGUAGE_KEY = "LanguageSelected";

    private LocalizationSettingsEntity localizationSettings;

    public void Initialize()
    {
        LoadSettings();
        AddListeners();
        ChangeLocaleByCode();
        view.Initialize(localizationSettings);
    }

    public void Conclude()
    {
        RemoveListeners();
        view.Conclude();
    }

    private void AddListeners() => localizationSettings.OnNewLanguageSelected += ChangeLocaleByCode;
    private void RemoveListeners() => localizationSettings.OnNewLanguageSelected -= ChangeLocaleByCode;

    public void ChangeLocaleByCode()
    {
        var locales = LocalizationSettings.AvailableLocales.Locales;

        foreach (var locale in locales)
        {
            if (locale.Identifier.Code == localizationSettings.CurrentLanguageCode)
            {
                LocalizationSettings.SelectedLocale = locale;
                SaveSettings();
                return;
            }
        }
    }

    public void SaveSettings() => PlayerPrefs.SetString(LANGUAGE_KEY, localizationSettings.CurrentLanguageCode);

    public void LoadSettings()
    {
        string savedLocale = PlayerPrefs.GetString(LANGUAGE_KEY, "en");
        localizationSettings = new LocalizationSettingsEntity(savedLocale);
    }
}
