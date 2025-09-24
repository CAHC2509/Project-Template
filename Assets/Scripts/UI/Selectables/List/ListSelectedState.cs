using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ListSelectedState : SelectableSelectedStateBase
{
    public ListSelectedState(SelectableData selectableData, Image[] mainImages, Image[] secondaryImages, TextMeshProUGUI[] texts) : base(selectableData, mainImages, secondaryImages, texts)
    {
    }
}
