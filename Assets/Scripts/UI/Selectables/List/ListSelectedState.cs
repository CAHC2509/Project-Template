using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ListSelectedState : SelectableSelectedStateBase
{
    public ListSelectedState(
        MonoBehaviour runner,
        SelectableData selectableData,
        Image[] mainImages,
        Image[] secondaryImages,
        TextMeshProUGUI[] texts)
        : base(runner, selectableData, mainImages, secondaryImages, texts) { }
}
