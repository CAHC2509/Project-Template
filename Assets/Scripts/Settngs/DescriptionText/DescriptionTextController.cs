using UnityEngine;
using TMPro;

public class DescriptionTextController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI descriptionText;

    private void OnEnable() => SelectableButton.OnButtonMessageSend += UpdateDescriptionText;

    private void OnDisable() => SelectableButton.OnButtonMessageSend -= UpdateDescriptionText;

    private void UpdateDescriptionText(string message) => descriptionText.text = message;
}
