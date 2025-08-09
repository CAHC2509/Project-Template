using System;
using UnityEngine.InputSystem;

[Serializable]
public class InputSettingsEntity
{
    public enum InputDeviceType { KeyboardMouse, Gamepad }

    public InputActionReference InputActionToRebind { get; private set; }
    public string InputActionToRebindName { get; private set; }
    public string InputActionToRebindKey { get; private set; }
    public InputDeviceType DeviceTypeToRebind { get; private set; }

    public event Action OnRebindRequest;
    public event Action OnRebindComplete;
    public event Action OnRebindInvalid;
    public event Action OnRebindCancel;
    public event Action OnRebindsReset;

    public void CreateNewRebindRequest(InputActionReference inputActionReference, string inputActionName, string inputActionKey, InputDeviceType deviceType)
    {
        InputActionToRebind = inputActionReference;
        InputActionToRebindName = inputActionName;
        InputActionToRebindKey = inputActionKey;
        DeviceTypeToRebind = deviceType;
        OnRebindRequest?.Invoke();
    }

    public void CompleteCurrentRebindRequest()
    {
        ClearReferences();
        OnRebindComplete?.Invoke();
    }

    public void InvalidateCurrentRebindRequest()
    {
        ClearReferences();
        OnRebindInvalid?.Invoke();
    }

    public void CancelCurrentRebindRequest()
    {
        ClearReferences();
        OnRebindCancel?.Invoke();
    }

    private void ClearReferences()
    {
        InputActionToRebind = null;
        InputActionToRebindName = string.Empty;
        InputActionToRebindKey = string.Empty;
    }

    public void ResetRebinds() => OnRebindsReset?.Invoke();
}
