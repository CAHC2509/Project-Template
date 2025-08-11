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

        localizedDescription.StringChanged += OnLocalizedDescriptionChanged;
        localizedDescription.RefreshString();
    }

    private void OnDestroy()
    {
        selectable.OnSelect -= SendDescriptionMessage;
        localizedDescription.StringChanged -= OnLocalizedDescriptionChanged;
    }

    private void SendDescriptionMessage()
    {
        OnDescriptionSent?.Invoke(localizedDescription.GetLocalizedString());
    }

    private void OnLocalizedDescriptionChanged(string value)
    {
        if (!string.IsNullOrEmpty(value))
            OnDescriptionSent?.Invoke(value);
    }
}
