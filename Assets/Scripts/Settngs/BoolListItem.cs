using System;
using UnityEngine;

public abstract class BoolListItem : SelectableListItem
{
    protected bool currentValue;

    protected override void PreviousItem()
    {
        if (currentValue == false) return;

        currentValue = false;
        ListChanged();
    }

    protected override void NextItem()
    {
        if (currentValue == true) return;

        currentValue = true;
        ListChanged();
    }

    protected void ListChanged()
    {
        OnListChanged?.Invoke();

        UpdateText();
        UpdateButtonStates();
        OnBoolValueChanged();
        AutoSelect();
    }

    protected void UpdateButtonStates()
    {
        previousButton.gameObject.SetActive(currentValue);
        nextButton.gameObject.SetActive(!currentValue);
    }

    protected void UpdateText() => itemText.text = currentValue ? "Yes" : "No";

    protected abstract void OnBoolValueChanged();
}
