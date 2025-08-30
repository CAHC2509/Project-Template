using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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
            StartCoroutine(SelectionWithDelay(defaultSelection.GetComponent<SelectableStateController>()));
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

    private IEnumerator SelectionWithDelay(SelectableStateController newSelectable)
    {
        yield return new WaitUntil(() => defaultSelection.gameObject.activeSelf);

        if (newSelectable != null)
            SelectionManager.SetNewSelectable?.Invoke(newSelectable);
    }
}
