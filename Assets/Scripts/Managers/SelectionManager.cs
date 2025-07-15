using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

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

    public void Select(SelectableStateController newSelection) => StartCoroutine(DelayedSelection(newSelection));

    private IEnumerator DelayedSelection(SelectableStateController newSelection)
    {
        if (currentSelection == newSelection)
            yield break;

        if (currentSelection != null && newSelection != null)
            Debug.Log($"{currentSelection.name} - {newSelection.name}");

        currentSelection?.Deselect();
        currentSelection = newSelection;

        yield return new WaitUntil(() => currentSelection.gameObject.activeInHierarchy);

        EventSystem.current.SetSelectedGameObject(currentSelection.gameObject);
        currentSelection.Select();
    }
}
