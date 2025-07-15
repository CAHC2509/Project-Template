using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ListSelectedState : ListState
{
    public ListSelectedState(SelectableData selectableData, Image[] mainImages, Image[] secondaryImages, TextMeshProUGUI[] texts)
    {
        this.selectableData = selectableData;
        this.mainImages = mainImages;
        this.secondaryImages = secondaryImages;
        this.texts = texts;
    }

    public override void Enter()
    {
        scaleTween = mainImages[0].transform.DOScale(selectableData.selectedScale, selectableData.animationDuration);

        foreach (Image image in mainImages)
            image.color = selectableData.backgroundSelectedColor;

        foreach (Image image in secondaryImages)
            image.color = selectableData.textSelectedColor;

        foreach (TextMeshProUGUI text in texts)
            text.color = selectableData.textSelectedColor;
    }

    public override void Exit() => scaleTween.Kill();
}
