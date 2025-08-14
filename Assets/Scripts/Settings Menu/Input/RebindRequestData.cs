using UnityEngine;
using UnityEngine.InputSystem;

public struct RebindRequestData
{
    public IndividualInputRebinder CurrentRebinder { get; private set; }
    public InputActionReference InputActionToRebind { get; private set; }
    public string InputActionToRebindName { get; private set; }
    public string InputActionToRebindKey { get; private set; }
    public InputDeviceType DeviceTypeToRebind { get; private set; }

    public RebindRequestData(
    IndividualInputRebinder currentRebinder,
    InputActionReference inputActionToRebind,
    string inputActionToRebindName,
    string inputActionToRebindKey,
    InputDeviceType deviceTypeToRebind)
    {
        CurrentRebinder = currentRebinder;
        InputActionToRebind = inputActionToRebind;
        InputActionToRebindName = inputActionToRebindName;
        InputActionToRebindKey = inputActionToRebindKey;
        DeviceTypeToRebind = deviceTypeToRebind;
    }
}

public enum InputDeviceType { KeyboardMouse, Gamepad }
