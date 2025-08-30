using System;

public interface ISettingsMenuController : IControllerBase
{
    public void ChangeCurrentPanel(SettingsSubmenuType submenuType);
    public void OpenMenuView();
    public void CloseMenuView();
}
