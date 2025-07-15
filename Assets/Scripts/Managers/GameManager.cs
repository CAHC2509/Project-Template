using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action OnInitialization;
    public static event Action OnFinalization;

    private void Start()
    {
        OnInitialization?.Invoke();
        AddListeners();
    }

    private void OnDestroy()
    {
        OnFinalization?.Invoke();
    }

    private void AddListeners()
    {
        MainMenuView.OnQuitPressed += QuitGame;
    }

    private void QuitGame() => Application.Quit();
}