using System;
using UnityEngine;

[Serializable]
public class SettingsMenuEntity
{
    public SelectableStateController RootSelectable { get; private set; }
    public SelectableStateController CurrentSelectable { get; private set; }
    public GameObject PreviousPanelSelected { get; private set; }
    public GameObject CurrentPanelSelected { get; private set; }
    public bool CurrentSelectableIsRoot { get; private set; }

    public event Action OnPanelChanged;
    public event Action OnSettingsMenuClosed;

    public void SetCurrentPanel(SelectableStateController rootSelectable, SelectableStateController newSelectable, GameObject newPanel)
    {
        RootSelectable = rootSelectable;
        CurrentSelectable = newSelectable;
        PreviousPanelSelected = CurrentPanelSelected;
        CurrentPanelSelected = newPanel;
        CurrentSelectableIsRoot = false;

        OnPanelChanged?.Invoke();
    }

    public void SetRootSelectable()
    {
        CurrentSelectable = RootSelectable;
        CurrentSelectableIsRoot = true;
        OnPanelChanged?.Invoke();
    }

    public void CloseSettingsMenu() => OnSettingsMenuClosed?.Invoke();
}

