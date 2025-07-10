using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSettingsEntity : MonoBehaviour
{
    public InputActionReference InputActionToRebind { get; private set; }

    public event Action OnRebindRequest;
    public event Action OnRebindComplete;
    public event Action OnRebindInvalid;
    public event Action OnRebindCancel;
    public event Action OnRebindsReset;

    public void CreateNewRebindRequest(InputActionReference inputActionReference)
    {
        InputActionToRebind = inputActionReference;
        OnRebindRequest?.Invoke();
    }

    public void CompleteCurrentRebindRequest()
    {
        InputActionToRebind = null;
        OnRebindComplete?.Invoke();
    }

    public void InvalidateCurrentRebindRequest()
    {
        InputActionToRebind = null;
        OnRebindInvalid?.Invoke();
    }

    public void CancelCurrentRebindRequest()
    {
        InputActionToRebind = null;
        OnRebindCancel?.Invoke();
    }

    public void ResetRebinds() => OnRebindsReset?.Invoke();
}
