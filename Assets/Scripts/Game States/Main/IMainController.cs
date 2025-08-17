using UnityEngine;

public interface IMainController : IControllerBase
{
    public void OpenSettingsMenu();
    public void StartGame();
    public void QuitGame();
}
