using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class SelectableSelectedStateBase : SelectableStateBase, IStaticState
{
    protected SelectableSelectedStateBase(SelectableData selectableData, Image[] mainImages, Image[] secondaryImages, TextMeshProUGUI[] texts)
    {
        this.selectableData = selectableData;
        this.mainImages = mainImages;
        this.secondaryImages = secondaryImages;
        this.texts = texts;
        
        container = mainImages[0].transform;
    }

    public virtual void Enter()
    {
        StartScaleAnimation(container, container.localScale, selectableData.selectedScale, selectableData.animationDuration);

        foreach (var image in mainImages)
            image.color = selectableData.backgroundSelectedColor;

        foreach (var image in secondaryImages)
            image.color = selectableData.textSelectedColor;

        foreach (var text in texts)
            text.color = selectableData.textSelectedColor;
    }

    public virtual void Exit()
    {
        StopScalingAnimation();
        container.localScale = selectableData.selectedScale;
    }
}
