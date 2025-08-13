using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocalizationSettingsController : ControllerBase, ISettingsController
{
    [SerializeField] private LocalizationSettingsView view;

    private const string LANGUAGE_KEY = "LanguageSelected";

    private LocalizationSettingsEntity localizationSettings;

    public void Dependencies()
    {
        LoadSettings();
        view.Dependencies(localizationSettings);
    }

    public override void Initialize()
    {
        base.Initialize();

        ChangeLocaleByCode();
        view.Initialize();
    }

    public override void Conclude()
    {
        base.Conclude();

        view.Conclude();
    }

    protected override void AddListeners() => localizationSettings.OnNewLanguageSelected += ChangeLocaleByCode;
    protected override void RemoveListeners() => localizationSettings.OnNewLanguageSelected -= ChangeLocaleByCode;

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
