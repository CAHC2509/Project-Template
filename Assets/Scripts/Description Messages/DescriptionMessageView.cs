using UnityEngine;
using TMPro;

public class DescriptionMessageView : ViewBase, IDescriptionMessageView
{
    [SerializeField] private TextMeshProUGUI descriptionText;

    protected override void AddPersistentListeners() => DescriptionMessageSender.OnDescriptionSent += UpdateDescriptionText;
    protected override void RemovePersistentListeners() => DescriptionMessageSender.OnDescriptionSent -= UpdateDescriptionText;
    private void UpdateDescriptionText(string description) => descriptionText.text = description;
}
