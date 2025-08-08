using System;
using UnityEngine;

public class MainEntity
{
    public event Action OnPlay;
    public event Action OnSettings;
    public event Action OnQuitRequested;
    public event Action OnQuitConfirmed;

    public void TriggerPlay() => OnPlay?.Invoke();
    public void TriggerSettings() => OnSettings?.Invoke();
    public void TriggerQuitRequest() => OnQuitRequested?.Invoke();
    public void TriggerQuitConfirmation() => OnQuitConfirmed?.Invoke();
}
