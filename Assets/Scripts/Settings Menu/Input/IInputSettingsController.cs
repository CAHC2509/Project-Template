public interface IInputSettingsController : IControllerBase, ISettingsSubmenuController
{
    public InputSettingsEntity GetModel();
    public void ResetToDefaults();
    public void CreateNewRebindRequest(RebindRequestData rebindRequestData);
}
