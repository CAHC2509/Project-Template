using System;

[Serializable]
public class GameplayEntity
{
    public event Action OnPause;
    public event Action OnUnPause;
    public event Action OnMatchStarted;
    public event Action OnMatchFinished;
    public event Action OnMainMenuRequest;

    private bool pauseState;

    public void TogglePause()
    {
        if (pauseState)
            UnPause();
        else
            Pause();
    }

    public void Pause()
    {
        pauseState = true;
        OnPause?.Invoke();
    }

    public void UnPause()
    {
        pauseState = false;
        OnUnPause?.Invoke();
    }

    public void StartMatch() => OnMatchStarted?.Invoke();
    public void FinishMatch() => OnMatchFinished?.Invoke();
    public void GoToMainMenu() => OnMainMenuRequest?.Invoke();
}
