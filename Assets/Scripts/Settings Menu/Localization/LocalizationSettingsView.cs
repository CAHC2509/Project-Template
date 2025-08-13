using UnityEngine;
using UnityEngine.UI;

public class LocalizationSettingsView : ViewBase
{
    [SerializeField] private Button englishButton;
    [SerializeField] private Button spanishButton;

    private ILocalizationSettingsController controller;

    private void Awake()
    {
        controller = GetComponentInParent<ILocalizationSettingsController>();
    }

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

    private void SelectEnglishLanguage() => controller.ChangeLocaleByCode("en");
    private void SelectSpanishLanguage() => controller.ChangeLocaleByCode("es");
}
