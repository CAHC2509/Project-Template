using System;
using UnityEngine;

public class MainController : UIControllerBase
{
    [SerializeField] private MainView view;

    public event Action OnPlay;
    public event Action OnSettings;
    public event Action OnQuit;

    public override void Initialize()
    {
        AddListeners();
        view.Initialize();
    }

    private void AddListeners()
    {
        view.OnPlayPressed += () => OnPlay?.Invoke();
        view.OnSettingsPressed += () => OnSettings?.Invoke();
        view.OnQuitPressed += () => OnQuit?.Invoke();
    }

    private void RemoveListeners()
    {
        view.OnPlayPressed -= () => OnPlay?.Invoke();
        view.OnSettingsPressed -= () => OnSettings?.Invoke();
        view.OnQuitPressed -= () => OnQuit?.Invoke();
    }

    public void OpenMainView() => view.EnableView();
    public void CloseMainView() => view.DisableView();
    public override void Conclude() => RemoveListeners();
}
