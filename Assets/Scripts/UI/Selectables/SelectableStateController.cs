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

    private void OnEnable()
    {
        button.onClick.AddListener(OnClickSelect);
        EventTrigger trigger = button.gameObject.AddComponent<EventTrigger>();

        var selectEntry = new EventTrigger.Entry { eventID = EventTriggerType.Select };
        selectEntry.callback.AddListener((data) => SelectionManager.Instance.Select(this));
        trigger.triggers.Add(selectEntry);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(OnClickSelect);
    }

    public void OnPointerEnter(PointerEventData eventData) => SelectionManager.Instance.Select(this);

    private void OnClickSelect() => SelectionManager.Instance.Select(this);

    public void Select()
    {
        OnSelect?.Invoke();
        ChangeState(selectedState);
    }

    public void Deselect()
    {
        OnDeselect?.Invoke();
        ChangeState(normalState);
    }
}
