using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class SelectableListItem : SelectableButton
{
    [Header("List components")]
    [SerializeField] protected TextMeshProUGUI itemText;
    [SerializeField] protected Button previousButton;
    [SerializeField] protected Button nextButton;

    public static Action OnListChanged;

    protected override void OnEnable()
    {
        base.OnEnable();

        previousButton.onClick.AddListener(PreviousItem);
        nextButton.onClick.AddListener(NextItem);

        UIInputHandler.OnLeft += HandlePrevious;
        UIInputHandler.OnRight += HandleNext;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        previousButton.onClick.RemoveListener(PreviousItem);
        nextButton.onClick.RemoveListener(NextItem);

        UIInputHandler.OnLeft -= HandlePrevious;
        UIInputHandler.OnRight -= HandleNext;
    }

    private void HandlePrevious()
    {
        if (!isSelected) return;
        PreviousItem();
    }

    private void HandleNext()
    {
        if (!isSelected) return;
        NextItem();
    }

    protected abstract void PreviousItem();

    protected abstract void NextItem();
}
