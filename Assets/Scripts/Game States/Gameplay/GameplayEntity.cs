using System;

[Serializable]
public class GameplayEntity
{
    public event Action OnPause;
    public event Action OnUnPause;
    public event Action OnMatchStarted;
    public event Action OnMatchFinished;
    public event Action OnMainMenuRequest;

    public void Pause() => OnPause?.Invoke();
    public void UnPause() => OnUnPause?.Invoke();
    public void StartMatch() => OnMatchStarted?.Invoke();
    public void FinishMatch() => OnMatchFinished?.Invoke();
    public void GoToMainMenu() => OnMainMenuRequest?.Invoke();
}
