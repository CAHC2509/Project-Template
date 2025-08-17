using System;

public interface ISettingsMenuController
{
    public event Action OnSettingsClosed;

    public void OpenSettingsView();
    public void CloseSettingsView();
}
