using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocalizationSettingsController : ControllerBase, ILocalizationSettingsController
{
    [SerializeField] private LocalizationSettingsView view;

    private ISaveSystem saveSystem;
    private LocalizationSettingsEntity localizationSettings;

    public void Dependencies(ISaveSystem saveSystem)
    {
        this.saveSystem = saveSystem;
    }

    public override void Initialize()
    {
        base.Initialize();

        LoadSettings();
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
        saveSystem.Save(Constants.Settings.LANGUAGE_KEY, localizationSettings);
    }

    public void LoadSettings()
    {
        if (saveSystem.HasKey(Constants.Settings.LANGUAGE_KEY))
            localizationSettings = (LocalizationSettingsEntity)saveSystem.Load(Constants.Settings.LANGUAGE_KEY, typeof(LocalizationSettingsEntity));
        else
            localizationSettings = new LocalizationSettingsEntity("en");

        ChangeLocaleByCode(localizationSettings.CurrentLanguageCode);
    }

    public void EnableView()
    {
        view.EnableView();
    }

    public void DisableView()
    {
        view.DisableView();
    }
}
