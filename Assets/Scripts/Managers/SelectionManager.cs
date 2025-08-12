using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    public static Action<SelectableStateController> SetNewSelectable;

    private SelectableStateController currentSelection;

    private void Awake() => SetNewSelectable += Select;
    private void OnDestroy() => SetNewSelectable -= Select;

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
