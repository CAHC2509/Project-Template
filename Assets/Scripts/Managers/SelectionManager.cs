using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager Instance { get; private set; }

    private SelectableStateController currentSelection;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    public void Select(SelectableStateController newSelection)
    {
        if (newSelection == null || !newSelection.gameObject.activeInHierarchy)
            return;

        if (currentSelection == newSelection)
            return;

        if (currentSelection != null)
            currentSelection.Deselect();

        currentSelection = newSelection;

        var rectTransform = currentSelection.GetComponent<RectTransform>();
        if (rectTransform != null)
            LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);

        if (EventSystem.current.currentSelectedGameObject == newSelection.gameObject)
        {
            currentSelection.Select();
            return;
        }

        if (!EventSystem.current.alreadySelecting)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(newSelection.gameObject);
            currentSelection.Select();
        }
    }
}
