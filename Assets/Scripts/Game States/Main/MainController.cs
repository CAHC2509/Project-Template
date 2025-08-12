using UnityEngine;

public class MainController : ControllerBase
{
    private MainEntity mainEntity;

    public void Dependencies(MainEntity mainEntity) => this.mainEntity = mainEntity;
    protected override void AddListeners() => mainEntity.OnQuitConfirmed += QuitGame;
    protected override void RemoveListeners() => mainEntity.OnQuitConfirmed -= QuitGame;
    private void QuitGame() => Application.Quit();
}
