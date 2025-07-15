using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class ListNormalState : ListState
{
    public ListNormalState(SelectableData selectableData, Image[] mainImages, Image[] secondaryImages, TextMeshProUGUI[] texts)
    {
        this.selectableData = selectableData;
        this.mainImages = mainImages;
        this.secondaryImages = secondaryImages;
        this.texts = texts;
    }

    public override void Enter()
    {
        scaleTween = mainImages[0].transform.DOScale(selectableData.normalScale, selectableData.animationDuration);

        foreach (Image image in mainImages)
            image.color = selectableData.backgroundNormalColor;

        foreach (Image image in secondaryImages)
            image.color = selectableData.textNormalColor;

        foreach (TextMeshProUGUI text in texts)
            text.color = selectableData.textNormalColor;
    }

    public override void Exit() => scaleTween.Kill();
}
