using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ButtonNormalState : SelectableNormalState
{
    public ButtonNormalState(SelectableData selectableData, Image[] mainImages, Image[] secondaryImages, TextMeshProUGUI[] texts) : base(selectableData, mainImages, secondaryImages, texts)
    {
    }
}
