using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderSelectedState : SelectableSelectedStateBase
{
    public SliderSelectedState(
        MonoBehaviour runner,
        SelectableData selectableData,
        Image[] mainImages,
        Image[] secondaryImages,
        TextMeshProUGUI[] texts)
        : base(runner, selectableData, mainImages, secondaryImages, texts) { }
}
