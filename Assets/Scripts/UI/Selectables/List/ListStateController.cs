using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ListStateController : SelectableStateController
{
    [SerializeField] private Image[] mainImages;
    [SerializeField] private Image[] secondaryImages;
    [SerializeField] private TextMeshProUGUI[] texts;

    public event Action OnPreviousItemRequested;
    public event Action OnNextItemRequested;

    private void Awake() => InitializeStateMachine();

    protected override void OnEnable()
    {
        base.OnEnable();
        AddListeners();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        RemoveListeners();
    }

    private void InitializeStateMachine()
    {
        normalState = new ListNormalState(selectableData, mainImages, secondaryImages, texts);
        selectedState = new ListSelectedState(selectableData, mainImages, secondaryImages, texts);
        Initialize(normalState);
    }

    private void AddListeners()
    {
        UIInputManager.OnLeft += RequestPreviousListItem;
        UIInputManager.OnRight += RequestNextListItem;
    }

    private void RemoveListeners()
    {
        UIInputManager.OnLeft -= RequestPreviousListItem;
        UIInputManager.OnRight -= RequestNextListItem;
    }

    private void RequestPreviousListItem()
    {
        if (currentState != selectedState) return;
        OnPreviousItemRequested?.Invoke();
    }

    private void RequestNextListItem()
    {
        if (currentState != selectedState) return;
        OnNextItemRequested?.Invoke();
    }
}
