using UnityEngine;
using UnityEngine.UI;

public class LocalizationSettingsView : ViewBase
{
    [SerializeField] private Button englishButton;
    [SerializeField] private Button spanishButton;

    private LocalizationSettingsEntity localizationSettings;

    public void Dependencies(LocalizationSettingsEntity localizationSettings) => this.localizationSettings = localizationSettings;

    protected override void AddPersistentListeners()
    {
        englishButton.onClick.AddListener(SelectEnglishLanguage);
        spanishButton.onClick.AddListener(SelectSpanishLanguage);
    }

    protected override void RemovePersistentListeners()
    {
        englishButton.onClick.RemoveListener(SelectEnglishLanguage);
        spanishButton.onClick.RemoveListener(SelectSpanishLanguage);
    }

    private void SelectEnglishLanguage() => localizationSettings.SelectNewLanguage("en");
    private void SelectSpanishLanguage() => localizationSettings.SelectNewLanguage("es");
}
