using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Action<GameStateBase> SetState;

    [SerializeField] private SettingsManager settingsManager;
    [SerializeField] private SceneLoaderController sceneLoader;
    [SerializeField] private States currentState;
    [SerializeField] private int targetFPS = 60;

    private GameStateBase currentGameState;

    private void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Application.targetFrameRate = targetFPS;
    }

    private void Start() => StartGame();

    private void StartGame()
    {
        AddListeners();
        sceneLoader.Initialize();
        sceneLoader.LoadScene(currentState.ToString());
    }

    private void AddListeners()
    {
        SetState += OnSetState;
    }

    private void OnSetState(GameStateBase state)
    {
        Debug.Log($"Current state: {state.name}");
        currentGameState = state;
        currentGameState.FinishState += OnChangeState;
        StateConfiguration(currentGameState);
    }

    private void StateConfiguration(GameStateBase state)
    {
        switch (state)
        {
            case MainState main:
                main.Dependencies(settingsManager);
                break;
        }
    }

    private void OnChangeState(States nextState)
    {
        Debug.Log($"Next state: {nextState}");

        currentGameState.FinishState -= OnChangeState;

        sceneLoader.UnloadScene(currentState.ToString());
        sceneLoader.LoadScene(nextState.ToString());

        currentState = nextState;
    }
}