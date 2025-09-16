using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ListStateController : SelectableStateController
{
    [SerializeField] private Image[] mainImages;
    [SerializeField] private Image[] secondaryImages;
    [SerializeField] private TextMeshProUGUI[] texts;
    [SerializeField] private AudioClip listModifiedSFX;

    public event Action OnPreviousItemRequested;
    public event Action OnNextItemRequested;

    private void Awake() => InitializeStateMachine();

    private void InitializeStateMachine()
    {
        normalState = new ListNormalState(this, selectableData, mainImages, secondaryImages, texts);
        selectedState = new ListSelectedState(this, selectableData, mainImages, secondaryImages, texts);
        Initialize(normalState);
    }

    protected override void AddTemporalListeners()
    {
        base.AddTemporalListeners();

        UIInputManager.OnLeft += RequestPreviousListItem;
        UIInputManager.OnRight += RequestNextListItem;
    }

    protected override void RemoveTemporalListeners()
    {
        base.RemoveTemporalListeners();

        UIInputManager.OnLeft -= RequestPreviousListItem;
        UIInputManager.OnRight -= RequestNextListItem;
    }

    private void RequestPreviousListItem()
    {
        if (currentState != selectedState) return;
        OnPreviousItemRequested?.Invoke();
        AudioManager.Instance.PlaySFX(listModifiedSFX);
    }

    private void RequestNextListItem()
    {
        if (currentState != selectedState) return;
        OnNextItemRequested?.Invoke();
        AudioManager.Instance.PlaySFX(listModifiedSFX);
    }
}
