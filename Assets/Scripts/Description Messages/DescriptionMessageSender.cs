using System;
using UnityEngine;
using UnityEngine.Localization;

[RequireComponent(typeof(SelectableStateController))]
public class DescriptionMessageSender : MonoBehaviour
{
    [Tooltip("Localization code related to this selectable functionalities")]
    [SerializeField] private LocalizedString localizedDescription;

    private SelectableStateController selectable;

    public static event Action<string> OnDescriptionSent;

    private void Awake()
    {
        selectable = GetComponent<SelectableStateController>();
        selectable.OnSelect += SendDescriptionMessage;
    }

    private void OnDestroy()
    {
        selectable.OnSelect -= SendDescriptionMessage;
    }

    private void SendDescriptionMessage()
    {
        string description = localizedDescription.GetLocalizedString();

        if (description != string.Empty)
            OnDescriptionSent?.Invoke(description);
    }
}
