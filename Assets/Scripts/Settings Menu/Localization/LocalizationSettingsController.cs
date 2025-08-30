using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocalizationSettingsController : ControllerBase, ILocalizationSettingsController
{
    [SerializeField] private LocalizationSettingsView view;

    private const string LANGUAGE_KEY = "LanguageSelected";

    private LocalizationSettingsEntity localizationSettings;

    private void Awake()
    {
        LoadSettings();
    }

    public override void Initialize()
    {
        base.Initialize();

        ChangeLocaleByCode(localizationSettings.CurrentLanguageCode);
        view.Initialize();
    }

    public override void Conclude()
    {
        base.Conclude();

        view.Conclude();
    }

    public void ChangeLocaleByCode(string languageCode)
    {
        var locales = LocalizationSettings.AvailableLocales.Locales;

        foreach (var locale in locales)
        {
            if (locale.Identifier.Code == languageCode)
            {
                localizationSettings.SetCurrentLanguage(languageCode);
                LocalizationSettings.SelectedLocale = locale;
                SaveSettings();
                return;
            }
        }
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetString(LANGUAGE_KEY, localizationSettings.CurrentLanguageCode);
        PlayerPrefs.Save();
    }

    public void LoadSettings()
    {
        string savedLocale = PlayerPrefs.GetString(LANGUAGE_KEY, "en");
        localizationSettings = new LocalizationSettingsEntity(savedLocale);
    }

    public void EnableView() => view.EnableView();
    public void DisableView() => view.DisableView();
}
