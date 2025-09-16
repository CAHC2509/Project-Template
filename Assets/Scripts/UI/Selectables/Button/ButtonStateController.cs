using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonStateController : SelectableStateController
{
    [SerializeField] private Image[] mainImages;
    [SerializeField] private Image[] secondaryImages;
    [SerializeField] private TextMeshProUGUI[] texts;
    [SerializeField] private AudioClip clickSFX;
    [SerializeField] private bool autoClick;

    private void Awake() => InitializeStateMachine();

    private void InitializeStateMachine()
    {
        normalState = new ButtonNormalState(selectableData, mainImages, secondaryImages, texts);
        selectedState = new ButtonSelectedState(selectableData, mainImages, secondaryImages, texts);
        Initialize(normalState);
    }

    protected override void AddTemporalListeners()
    {
        base.AddTemporalListeners();

        button.onClick.AddListener(OnButtonCLicked);
    }

    protected override void RemoveTemporalListeners()
    {
        base.RemoveTemporalListeners();

        button.onClick.RemoveListener(OnButtonCLicked);
    }

    private void OnButtonCLicked() => AudioManager.Instance.PlaySFX(clickSFX);

    public override void Select()
    {
        base.Select();

        if (autoClick)
            button.onClick.Invoke();
    }
}
