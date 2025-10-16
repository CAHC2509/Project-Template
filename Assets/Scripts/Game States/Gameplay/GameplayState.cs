using UnityEngine;
using Unity.Cinemachine;

public class GameplayState : GameStateBase, IGameplayState
{
    [Header("Player settings")]
    [SerializeField] private PlayerController playerPrefab;
    [SerializeField] private Transform playerSpawn;

    [Header("Camera settings")]
    [SerializeField] private CinemachineCamera cinemachineCamera;

    [Header("Gameplay settings")]
    [SerializeField] private GameplayController controller;

    private SettingsManager settingsManager;
    private PlayerController player;

    public void Dependencies(SettingsManager settingsManager)
    {
        this.settingsManager = settingsManager;
        controller.Dependencies(this, settingsManager);

        EnterState();
    }

    protected override void EnterState()
    {
        base.EnterState();

        player = Instantiate(playerPrefab, playerSpawn.position, playerSpawn.rotation);
        player.Initialize();

        cinemachineCamera.Follow = player.transform;

        controller.Initialize();
    }

    protected override void ExitState()
    {
        base.ExitState();

        controller.Conclude();
        player.Conclude();
        Destroy(player.gameObject);
    }

    public void LoadMainMenu()
    {
        nextState = States.Main;
        ExitState();
    }

    public void LoadResults()
    {
        nextState = States.Results;
        ExitState();
    }
}
