using System;
using UnityEngine;

[System.Serializable]
public class SettingsMenuEntity
{
    public SelectableStateController RootSelectable { get; private set; }
    public SelectableStateController CurrentSelectable { get; private set; }
    public GameObject CurrentPanelSelected { get; private set; }
    public bool CurrentSelectableIsRoot { get; private set; }

    public event Action OnNewPanelSelected;

    public void SelectNewPanel(SelectableStateController rootSelectable, SelectableStateController newSelectable, GameObject newPanel)
    {
        if (CurrentPanelSelected != null)
            CurrentPanelSelected.SetActive(false);

        RootSelectable = rootSelectable;
        CurrentSelectable = newSelectable;
        CurrentPanelSelected = newPanel;

        CurrentSelectableIsRoot = false;

        OnNewPanelSelected?.Invoke();
    }

    public void ExitFromCurrentPanel()
    {
        CurrentSelectable = RootSelectable;
        SelectionManager.Instance.Select(CurrentSelectable);

        CurrentSelectableIsRoot = true;
    }
}
