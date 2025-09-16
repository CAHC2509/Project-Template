using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ButtonSelectedState : SelectableSelectedStateBase
{
    public ButtonSelectedState(MonoBehaviour runner,
        SelectableData selectableData,
        Image[] mainImages,
        Image[] secondaryImages,
        TextMeshProUGUI[] texts)
        : base(runner, selectableData, mainImages, secondaryImages, texts) { }
}
