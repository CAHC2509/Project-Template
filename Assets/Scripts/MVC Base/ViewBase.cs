using UnityEngine;
using UnityEngine.UI;

public abstract class ViewBase : MonoBehaviour
{
    [SerializeField] protected GameObject viewContainer;

    protected Button defaultSelection;

    public virtual void Initialize() => AddPersistentListeners();
    public virtual void Conclude() => RemovePersistentListeners();

    protected virtual void AddPersistentListeners() { }
    protected virtual void RemovePersistentListeners() { }
    protected virtual void AddTemporaryListeners() { }
    protected virtual void RemoveTemporaryListeners() { }

    protected virtual void SetDefaultSelection()
    {
        if (defaultSelection != null)
            SelectionManager.SetNewSelectable?.Invoke(defaultSelection.GetComponent<SelectableStateController>());
    }

    public virtual void EnableView()
    {
        viewContainer.SetActive(true);
        SetDefaultSelection();
        AddTemporaryListeners();
    }

    public virtual void DisableView()
    {
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
}
