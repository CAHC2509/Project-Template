using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GraphicsSettingsView : ViewBase
{
    [Header("Resolution")]
    [SerializeField] private ListStateController resolutionListController;
    [SerializeField] private Button previousResolutionButton;
    [SerializeField] private Button nextResolutionButton;
    [SerializeField] private TextMeshProUGUI resolutionText;

    [Space, Header("Quality")]
    [SerializeField] private ListStateController qualityListController;
    [SerializeField] private Button previousQualityButton;
    [SerializeField] private Button nextQualityButton;
    [SerializeField] private TextMeshProUGUI qualityLevelText;

    [Space, Header("Full Screen")]
    [SerializeField] private ListStateController fullScreenListController;
    [SerializeField] private Button previousFullScreenButton;
    [SerializeField] private Button nextFullScreenButton;
    [SerializeField] private TextMeshProUGUI fullScreenModeText;

    private GraphicsSettingsEntity graphicsSettings;

    public void Dependencies(GraphicsSettingsEntity graphicsSettings) => this.graphicsSettings = graphicsSettings;

    public override void Initialize()
    {
        AddPersistentListeners();
        InitializeSelectors();
    }

    public override void Conclude()
    {
        RemovePersistentListeners();
    }

    protected override void AddPersistentListeners()
    {
        previousResolutionButton.onClick.AddListener(PreviousResolution);
        nextResolutionButton.onClick.AddListener(NextResolution);

        previousQualityButton.onClick.AddListener(PreviousQualityLevel);
        nextQualityButton.onClick.AddListener(NextQualityLevel);

        previousFullScreenButton.onClick.AddListener(DisableFullScreenMode);
        nextFullScreenButton.onClick.AddListener(EnableFullScreenMode);

        resolutionListController.OnPreviousItemRequested += PreviousResolution;
        resolutionListController.OnNextItemRequested += NextResolution;

        qualityListController.OnPreviousItemRequested += PreviousQualityLevel;
        qualityListController.OnNextItemRequested += NextQualityLevel;

        fullScreenListController.OnPreviousItemRequested += DisableFullScreenMode;
        fullScreenListController.OnNextItemRequested += EnableFullScreenMode;

        graphicsSettings.OnResolutionChanged += UpdateResolutionButtons;
        graphicsSettings.OnResolutionChanged += UpdateResolutionText;
        graphicsSettings.OnQualityLevelChanged += UpdateQualityLevelsButtons;
        graphicsSettings.OnQualityLevelChanged += UpdateQualityLevelText;
        graphicsSettings.OnFullScreenChanged += UpdateFullScreenModeButtons;
        graphicsSettings.OnFullScreenChanged += UpdateFullScreenModeText;
    }

    protected override void RemovePersistentListeners()
    {
        previousResolutionButton.onClick.RemoveListener(PreviousResolution);
        nextResolutionButton.onClick.RemoveListener(NextResolution);

        previousQualityButton.onClick.RemoveListener(PreviousQualityLevel);
        nextQualityButton.onClick.RemoveListener(NextQualityLevel);

        previousFullScreenButton.onClick.RemoveListener(DisableFullScreenMode);
        nextFullScreenButton.onClick.RemoveListener(EnableFullScreenMode);

        resolutionListController.OnPreviousItemRequested -= PreviousResolution;
        resolutionListController.OnNextItemRequested -= NextResolution;

        qualityListController.OnPreviousItemRequested -= PreviousQualityLevel;
        qualityListController.OnNextItemRequested -= NextQualityLevel;

        fullScreenListController.OnPreviousItemRequested -= DisableFullScreenMode;
        fullScreenListController.OnNextItemRequested -= EnableFullScreenMode;

        graphicsSettings.OnResolutionChanged -= UpdateResolutionButtons;
        graphicsSettings.OnResolutionChanged -= UpdateResolutionText;
        graphicsSettings.OnQualityLevelChanged -= UpdateQualityLevelsButtons;
        graphicsSettings.OnQualityLevelChanged -= UpdateQualityLevelText;
        graphicsSettings.OnFullScreenChanged -= UpdateFullScreenModeButtons;
        graphicsSettings.OnFullScreenChanged -= UpdateFullScreenModeText;
    }

    private void InitializeSelectors()
    {
        UpdateResolutionButtons();
        UpdateResolutionText();

        UpdateQualityLevelsButtons();
        UpdateQualityLevelText();

        UpdateFullScreenModeButtons();
        UpdateFullScreenModeText();
    }

    private void PreviousResolution()
    {
        int currentIndex = graphicsSettings.CurrentResolutionIndex - 1;
        graphicsSettings.SetResolutionIndex(currentIndex);
    }

    private void NextResolution()
    {
        int currentIndex = graphicsSettings.CurrentResolutionIndex + 1;
        graphicsSettings.SetResolutionIndex(currentIndex);
    }

    private void PreviousQualityLevel()
    {
        int currentIndex = graphicsSettings.CurrentQualityLevelIndex - 1;
        graphicsSettings.SetQualityLevelIndex(currentIndex);
    }

    private void NextQualityLevel()
    {
        int currentIndex = graphicsSettings.CurrentQualityLevelIndex + 1;
        graphicsSettings.SetQualityLevelIndex(currentIndex);
    }

    private void EnableFullScreenMode() => graphicsSettings.SetFullscreenMode(true);

    private void DisableFullScreenMode() => graphicsSettings.SetFullscreenMode(false);

    private void UpdateResolutionButtons()
    {
        int currentIndex = graphicsSettings.CurrentResolutionIndex;
        previousResolutionButton.gameObject.SetActive(!(currentIndex <= 0));
        nextResolutionButton.gameObject.SetActive(!(currentIndex >= graphicsSettings.AvailableResolutions.Count - 1));
    }

    private void UpdateQualityLevelsButtons()
    {
        int currentIndex = graphicsSettings.CurrentQualityLevelIndex;
        previousQualityButton.gameObject.SetActive(!(currentIndex <= 0));
        nextQualityButton.gameObject.SetActive(!(currentIndex >= graphicsSettings.AvailableQualityLevels.Count - 1));
    }

    private void UpdateFullScreenModeButtons()
    {
        bool activeMode = graphicsSettings.CurrentFullScreenMode;
        previousFullScreenButton.gameObject.SetActive(activeMode);
        nextFullScreenButton.gameObject.SetActive(!activeMode);
    }

    private void UpdateResolutionText()
    {
        Resolution currentResolution = graphicsSettings.AvailableResolutions[graphicsSettings.CurrentResolutionIndex];
        resolutionText.text = $"{currentResolution.width}x{currentResolution.height}";
    }

    private void UpdateQualityLevelText()
    {
        string currentQualityLevel = graphicsSettings.AvailableQualityLevels[graphicsSettings.CurrentQualityLevelIndex];
        qualityLevelText.text = currentQualityLevel;
    }

    private void UpdateFullScreenModeText()
    {
        string currentFullScreenMode = graphicsSettings.CurrentFullScreenMode ? "Yes" : "No";
        fullScreenModeText.text = currentFullScreenMode;
    }
}
