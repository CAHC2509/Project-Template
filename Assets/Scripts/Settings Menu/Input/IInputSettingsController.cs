public interface IInputSettingsController : IControllerBase
{
    public InputSettingsEntity GetModel();
    public void ResetToDefaults();
    public void CreateNewRebindRequest(RebindRequestData rebindRequestData);
}
