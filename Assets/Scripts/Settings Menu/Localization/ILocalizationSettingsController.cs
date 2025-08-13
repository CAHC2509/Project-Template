using UnityEngine;

public interface ILocalizationSettingsController : IControllerBase, ISettingsController
{
    public void ChangeLocaleByCode(string languageCode);
}
