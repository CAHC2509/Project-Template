using UnityEngine;

public interface IGraphicsSettingsController : ISettingsController
{
    public GraphicsSettingsEntity GetModel();
    public void SetResolution(int index);
    public void SetQualityLevel(int index);
    public void SetFullScreenMode(bool mode);
}
