using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class SelectableNormalState : SelectableStateBase, IStaticState
{
    protected SelectableNormalState(SelectableData selectableData, Image[] mainImages, Image[] secondaryImages, TextMeshProUGUI[] texts)
    {
        this.selectableData = selectableData;
        this.mainImages = mainImages;
        this.secondaryImages = secondaryImages;
        this.texts = texts;
        
        container = mainImages[0].transform;
    }

    public virtual void Enter()
    {
        container.localScale = selectableData.normalScale;

        foreach (var image in mainImages)
            image.color = selectableData.backgroundNormalColor;

        foreach (var image in secondaryImages)
            image.color = selectableData.textNormalColor;

        foreach (var text in texts)
            text.color = selectableData.textNormalColor;
    }

    public virtual void Exit()
    {
        StopScalingAnimation();
        container.localScale = selectableData.normalScale;
    }
}
