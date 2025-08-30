using UnityEngine;

public interface ILocalizationSettingsController : IControllerBase, ISettingsSubmenuController
{
    public void ChangeLocaleByCode(string languageCode);
}
