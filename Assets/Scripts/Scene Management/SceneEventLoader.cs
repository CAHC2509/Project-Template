using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneEventLoader : MonoBehaviour
{
    [SerializeField] private List<SceneField> scenesToLoad;
    [SerializeField] private Button eventButton;

    private void Awake() => GameManager.OnInitialization += OnInitialization;

    private void OnInitialization() => AddListeners();

    private void AddListeners() => eventButton.onClick.AddListener(StartSwapingScenes);

    private void StartSwapingScenes() => SceneSwapManager.Instance.LoadAdditiveScenes(scenesToLoad);
}
