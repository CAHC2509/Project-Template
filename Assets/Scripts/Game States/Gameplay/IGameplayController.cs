using UnityEngine;

public interface IGameplayController : IControllerBase
{
    public void PauseGame();
    public void UnPauseGame();
    public void FinishMatch();
    public void GoToMainMenu();
    public void OpenSettingsMenu();
}
