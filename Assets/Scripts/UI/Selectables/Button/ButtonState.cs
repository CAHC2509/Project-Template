using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public abstract class ButtonState : IStaticState
{
    protected SelectableData selectableData;
    protected Button button;
    protected Image[] mainImages;
    protected Image[] secondaryImages;
    protected TextMeshProUGUI[] texts;
    protected Tween scaleTween;

    public abstract void Enter();
    public abstract void Exit();
}
