using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MEC;

public abstract class ViewBase : MonoBehaviour, IViewBase
{
    [SerializeField] protected GameObject viewContainer;

    protected Button defaultSelection;

    public virtual void Initialize()
    {
        AddPersistentListeners();
    }

    public virtual void Conclude()
    {
        RemoveTemporaryListeners();
        RemovePersistentListeners();
    }

    protected virtual void AddPersistentListeners() { }
    protected virtual void RemovePersistentListeners() { }
    protected virtual void AddTemporaryListeners() { }
    protected virtual void RemoveTemporaryListeners() { }

    protected virtual void SetDefaultSelection()
    {
        if (defaultSelection != null)
            Timing.RunCoroutine(SelectionWithDelay(defaultSelection.GetComponent<SelectableStateController>()));
    }

    public virtual void EnableView()
    {
        if (viewContainer != null)
            viewContainer.SetActive(true);

        SetDefaultSelection();
        AddTemporaryListeners();
    }

    public virtual void DisableView()
    {
        if (viewContainer != null)
            viewContainer.SetActive(false);

        RemoveTemporaryListeners();
    }

    public virtual void ToggleView()
    {
        if (!viewContainer.activeSelf)
            EnableView();
        else
            DisableView();
    }

    private IEnumerator<float> SelectionWithDelay(SelectableStateController newSelectable)
    {
        while (!defaultSelection.gameObject.activeSelf)
            yield return Timing.WaitForOneFrame;

        if (newSelectable != null)
            SelectionManager.SetNewSelectable?.Invoke(newSelectable);
    }
}
