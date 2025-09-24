using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ListNormalState : SelectableNormalState
{
    public ListNormalState(SelectableData selectableData, Image[] mainImages, Image[] secondaryImages, TextMeshProUGUI[] texts) : base(selectableData, mainImages, secondaryImages, texts)
    {
    }
}
