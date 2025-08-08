using UnityEngine;
using UnityEngine.UI;

public abstract class ViewBase : MonoBehaviour
{
    [SerializeField] protected GameObject viewContainer;

    protected Button defaultSelection;

    public abstract void Initialize();
    public abstract void Conclude();

    protected virtual void SetDefaultSelection()
    {
        if (defaultSelection != null)
            SelectionManager.Instance.Select(defaultSelection.GetComponent<SelectableStateController>());
    }

    public virtual void EnableView()
    {
        viewContainer.SetActive(true);
        SetDefaultSelection();
    }

    public virtual void ToggleView()
    {
        viewContainer.SetActive(!viewContainer.activeSelf);

        if (viewContainer.activeSelf)
            SetDefaultSelection();
    }

    public virtual void DisableView() => viewContainer.SetActive(false);
}
