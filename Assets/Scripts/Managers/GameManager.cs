using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Action<GameStateBase> SetState;

    [SerializeField] private AudioManager audioManager;
    [SerializeField] private SelectionManager selectionManager;
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

    private void Start()
    {
        Initialize();
    }

    private void OnApplicationQuit()
    {
        Conclude();
    }

    private void Initialize()
    {
        audioManager.Initialize();

        selectionManager.Initialize();

        settingsManager.Dependencies();
        settingsManager.Initialize();

        sceneLoader.Dependencies();
        sceneLoader.Initialize();
        sceneLoader.LoadScene(currentState.ToString());

        AddListeners();
    }

    private void Conclude()
    {
        audioManager.Conclude();
        selectionManager.Conclude();
        settingsManager.Conclude();
        sceneLoader.Conclude();

        RemoveListeners();
    }

    private void AddListeners()
    {
        SetState += OnSetState;
    }

    private void RemoveListeners()
    {
        SetState -= OnSetState;
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
            case OnboardingState onboarding:
                onboarding.Dependencies(settingsManager);
                break;
            case GameplayState gameplay:
                gameplay.Dependencies(settingsManager);
                break;
            case ResultsState results:
                results.Dependencies();
                break;
        }
    }

    private void OnChangeState(States nextState)
    {
        Debug.Log($"Next state: {nextState}");

        currentGameState.FinishState -= OnChangeState;

        if (currentState == nextState)
        {
            sceneLoader.ReloadScene(nextState.ToString());
        }
        else
        {
            sceneLoader.UnloadScene(currentState.ToString());
            sceneLoader.LoadScene(nextState.ToString());
        }

        currentState = nextState;
    }
}