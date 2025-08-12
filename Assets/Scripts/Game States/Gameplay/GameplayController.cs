using UnityEngine;

public class GameplayController : ControllerBase
{
    private GameplayEntity gameplayEntity;
    private float lastTimeScale = 1f;

    public void Dependencies(GameplayEntity gameplayEntity) => this.gameplayEntity = gameplayEntity;

    public override void Initialize()
    {
        base.Initialize();

        UnPauseGame();
    }

    public override void Conclude()
    {
        base.Conclude();

        UnPauseGame();
    }

    protected override void AddListeners()
    {
        gameplayEntity.OnPause += PauseGame;
        gameplayEntity.OnUnPause += UnPauseGame;
        gameplayEntity.OnMainMenuRequest += UnPauseGame;
    }

    protected override void RemoveListeners()
    {
        gameplayEntity.OnPause -= PauseGame;
        gameplayEntity.OnUnPause -= UnPauseGame;
        gameplayEntity.OnMainMenuRequest -= UnPauseGame;
    }

    private void PauseGame()
    {
        lastTimeScale = Time.timeScale;
        Time.timeScale = 0f;
    }

    private void UnPauseGame() => Time.timeScale = lastTimeScale;
}
