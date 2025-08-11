using UnityEngine;
using TMPro;

public class DescriptionMessageView : ViewBase
{
    [SerializeField] private TextMeshProUGUI descriptionText;

    public override void Initialize() => AddPersistentListeners();
    public override void Conclude() => RemovePersistentListeners();

    protected override void AddPersistentListeners() => DescriptionMessageSender.OnDescriptionSent += UpdateDescriptionText;
    protected override void RemovePersistentListeners() => DescriptionMessageSender.OnDescriptionSent -= UpdateDescriptionText;
    private void UpdateDescriptionText(string description) => descriptionText.text = description;
}
