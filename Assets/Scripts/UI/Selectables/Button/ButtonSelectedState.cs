using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ButtonSelectedState : SelectableSelectedStateBase
{
    public ButtonSelectedState(SelectableData selectableData, Image[] mainImages, Image[] secondaryImages, TextMeshProUGUI[] texts) : base(selectableData, mainImages, secondaryImages, texts)
    {
    }
}
