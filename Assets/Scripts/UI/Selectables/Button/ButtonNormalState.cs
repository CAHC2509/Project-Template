using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ButtonNormalState : SelectableNormalState
{
    public ButtonNormalState(MonoBehaviour runner,
        SelectableData selectableData,
        Image[] mainImages,
        Image[] secondaryImages,
        TextMeshProUGUI[] texts)
        : base(runner, selectableData, mainImages, secondaryImages, texts) { }
}
