using UnityEngine;

public interface IInputSettingsView : IViewBase
{
    public void ShowRebindWindow();
    public void HideRebindWindow();
    public void ShowInvalidRebindWindow();
}
