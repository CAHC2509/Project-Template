using UnityEngine;

public interface IGraphicsSettingsController : ISettingsSubmenuController
{
    public GraphicsSettingsEntity GetModel();
    public void SetResolution(int index);
    public void SetQualityLevel(int index);
    public void SetFullScreenMode(bool mode);
}
