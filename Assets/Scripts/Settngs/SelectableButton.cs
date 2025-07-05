using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

[RequireComponent(typeof(Button))]
public class SelectableButton : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler
{
    [SerializeField] protected GameObject selectionHint;
    [SerializeField] protected DescriptionMessage descriptionMessage;
    [SerializeField] protected Vector3 selectedScale = new Vector3(1.05f, 1.05f, 1.05f);
    [SerializeField] protected float animationTime = 0.15f;

    public static event Action<string> OnButtonMessageSend;
    public static event Action OnButtonSelected;
    public static event Action OnButtonClicked;

    protected bool isSelected;
    protected Button button;
    protected Vector3 defaultScale;

    protected virtual void Awake()
    {
        button = GetComponent<Button>();
        defaultScale = button.transform.localScale;
    }

    protected virtual void OnEnable()
    {
        button.onClick.AddListener(TriggerClickEvent);
    }

    protected virtual void OnDisable()
    {
        button.onClick.RemoveListener(TriggerClickEvent);
        SetDefaultState();
    }

    public void OnSelect(BaseEventData eventData) => SetSelectedState();

    public void OnDeselect(BaseEventData eventData) => SetDefaultState();

    public void OnPointerEnter(PointerEventData eventData) => AutoSelect();

    private void TriggerClickEvent() => OnButtonClicked?.Invoke();
    
    protected void AutoSelect() => EventSystem.current.SetSelectedGameObject(gameObject);

    protected virtual void SetSelectedState()
    {
        isSelected = true;

        if (selectionHint != null)
            selectionHint.SetActive(true);

        if (button != null)
        {
            button.transform.DOKill();
            button.transform.DOScale(selectedScale, animationTime).SetEase(Ease.OutBack);
        }

        if (descriptionMessage != null)
            OnButtonMessageSend?.Invoke(descriptionMessage.Message);

        OnButtonSelected?.Invoke();
    }

    protected virtual void SetDefaultState()
    {
        isSelected = false;

        if (selectionHint != null)
            selectionHint.SetActive(false);

        if (button != null)
        {
            button.transform.DOKill();
            button.transform.DOScale(defaultScale, animationTime).SetEase(Ease.OutBack);
        }
    }
}
