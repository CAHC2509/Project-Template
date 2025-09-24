using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderSelectedState : SelectableSelectedStateBase
{
    public SliderSelectedState(SelectableData selectableData, Image[] mainImages, Image[] secondaryImages, TextMeshProUGUI[] texts) : base(selectableData, mainImages, secondaryImages, texts)
    {
    }
}
