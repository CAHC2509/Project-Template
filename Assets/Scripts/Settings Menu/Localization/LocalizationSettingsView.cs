using UnityEngine;
using UnityEngine.UI;

public class LocalizationSettingsView : MonoBehaviour
{
    [SerializeField] private Button englishButton;
    [SerializeField] private Button spanishButton;

    private LocalizationSettingsEntity localizationSettings;

    public void Initialize(LocalizationSettingsEntity localizationSettings)
    {
        this.localizationSettings = localizationSettings;
        AddListeners();
    }

    public void Conclude()
    {
        RemoveListeners();
    }

    private void AddListeners()
    {
        englishButton.onClick.AddListener(SelectEnglishLanguage);
        spanishButton.onClick.AddListener(SelectSpanishLanguage);
    }

    private void RemoveListeners()
    {
        englishButton.onClick.RemoveListener(SelectEnglishLanguage);
        spanishButton.onClick.RemoveListener(SelectSpanishLanguage);
    }

    private void SelectEnglishLanguage() => localizationSettings.SelectNewLanguage("en");
    private void SelectSpanishLanguage() => localizationSettings.SelectNewLanguage("es");
}
