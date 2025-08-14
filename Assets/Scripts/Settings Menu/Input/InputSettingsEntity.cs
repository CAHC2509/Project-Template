using System;
using UnityEngine.InputSystem;

[Serializable]
public class InputSettingsEntity
{    
    public IndividualInputRebinder CurrentRebinder { get; private set; }
    public InputActionReference InputActionToRebind { get; private set; }
    public string InputActionToRebindName { get; private set; }
    public string InputActionToRebindKey { get; private set; }
    public InputDeviceType DeviceTypeToRebind { get; private set; }

    public void CreateNewRebindRequest(RebindRequestData rebindRequestData)
    {
        CurrentRebinder = rebindRequestData.CurrentRebinder;
        InputActionToRebind = rebindRequestData.InputActionToRebind;
        InputActionToRebindName = rebindRequestData.InputActionToRebindName;
        InputActionToRebindKey = rebindRequestData.InputActionToRebindKey;
        DeviceTypeToRebind = rebindRequestData.DeviceTypeToRebind;
    }

    public void ClearReferences()
    {
        CurrentRebinder = null;
        InputActionToRebind = null;
        InputActionToRebindName = string.Empty;
        InputActionToRebindKey = string.Empty;
    }
}
