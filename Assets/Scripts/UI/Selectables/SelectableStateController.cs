using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectableStateController : StaticStateMachine, IPointerEnterHandler
{
    [SerializeField] protected SelectableData selectableData;
    [SerializeField] protected Button button;

    protected IStaticState normalState;
    protected IStaticState selectedState;

    public Button Button => button;
    public event Action OnSelect;
    public event Action OnDeselect;

    protected virtual void OnEnable()
    {
        button.onClick.AddListener(OnClickSelect);
        EventTrigger trigger = button.gameObject.AddComponent<EventTrigger>();

        var selectEntry = new EventTrigger.Entry { eventID = EventTriggerType.Select };
        selectEntry.callback.AddListener((data) => SelectionManager.SetNewSelectable?.Invoke(this));
        trigger.triggers.Add(selectEntry);
    }

    protected virtual void OnDisable() => button.onClick.RemoveListener(OnClickSelect);
    public void OnPointerEnter(PointerEventData eventData) => SelectionManager.SetNewSelectable?.Invoke(this);
    private void OnClickSelect() => SelectionManager.SetNewSelectable?.Invoke(this);

    public virtual void Select()
    {
        OnSelect?.Invoke();
        ChangeState(selectedState);
    }

    public virtual void Deselect()
    {
        OnDeselect?.Invoke();
        ChangeState(normalState);
    }
}
