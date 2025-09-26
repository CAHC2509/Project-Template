using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class SelectableStateController : StaticStateMachine, IPointerEnterHandler
{
    [SerializeField] protected SelectableData selectableData;
    [SerializeField] protected Button button;
    [SerializeField] protected AudioData selectionSFX;

    protected IStaticState normalState;
    protected IStaticState selectedState;

    public Button Button => button;
    public event Action OnSelect;
    public event Action OnDeselect;

    private void OnEnable() => AddTemporalListeners();
    private void OnDisable() => RemoveTemporalListeners();
    public void OnPointerEnter(PointerEventData eventData) => SelectionManager.SetNewSelectable?.Invoke(this);
    private void OnClickSelect() => SelectionManager.SetNewSelectable?.Invoke(this);

    public virtual void Select()
    {
        OnSelect?.Invoke();
        ChangeState(selectedState);
        AudioManager.PlayUISFX(selectionSFX);
    }

    public virtual void Deselect()
    {
        ChangeState(normalState);
        OnDeselect?.Invoke();
    }

    protected virtual void AddTemporalListeners()
    {
        button.onClick.AddListener(OnClickSelect);
        EventTrigger trigger = button.gameObject.AddComponent<EventTrigger>();

        var selectEntry = new EventTrigger.Entry { eventID = EventTriggerType.Select };
        selectEntry.callback.AddListener((data) => SelectionManager.SetNewSelectable?.Invoke(this));
        trigger.triggers.Add(selectEntry);
    }

    protected virtual void RemoveTemporalListeners()
    {
        button.onClick.RemoveListener(OnClickSelect);
    }
}
