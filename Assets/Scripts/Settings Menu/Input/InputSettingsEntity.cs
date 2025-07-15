using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSettingsEntity : MonoBehaviour
{
    public InputActionReference InputActionToRebind { get; private set; }
    public string InputActionToRebindName { get; private set; }

    public event Action OnRebindRequest;
    public event Action OnRebindComplete;
    public event Action OnRebindInvalid;
    public event Action OnRebindCancel;
    public event Action OnRebindsReset;

    public void CreateNewRebindRequest(InputActionReference inputActionReference, string inputActionName)
    {
        InputActionToRebind = inputActionReference;
        InputActionToRebindName = inputActionName;
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
    }

    public void ResetRebinds() => OnRebindsReset?.Invoke();
}
