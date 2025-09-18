using UnityEngine;

public interface IMainController : IControllerBase
{
    public void StartGame();
    public void StartTutorial();
    public void OpenSettingsMenu();
    public void QuitGame();
}
