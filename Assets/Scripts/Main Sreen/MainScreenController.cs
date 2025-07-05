using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainScreenController : MonoBehaviour
{
    [SerializeField] private GameObject defaultSelection;
    [SerializeField] private GameObject homeCanvas;
    [SerializeField] private GameObject settingsCanvas;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button exitButton;

    private void OnEnable()
    {
        settingsButton.onClick.AddListener(EnableSettingsCanvas);
        settingsButton.onClick.AddListener(DisableHomeCanvas);

        exitButton.onClick.AddListener(QuitGame);

        SettingsMenu.OnSettingsClose += EnableHomeCanvas;
        SettingsMenu.OnSettingsClose += SelectDefaultItem;

        SelectDefaultItem();
    }

    private void OnDisable()
    {
        settingsButton.onClick.RemoveListener(EnableSettingsCanvas);
        settingsButton.onClick.RemoveListener(DisableHomeCanvas);

        exitButton.onClick.RemoveListener(QuitGame);

        SettingsMenu.OnSettingsClose -= EnableHomeCanvas;
        SettingsMenu.OnSettingsClose -= SelectDefaultItem;
    }

    private void SelectDefaultItem() => StartCoroutine(SelectDefaultItemCoroutine());
    private void EnableHomeCanvas() => homeCanvas.SetActive(true);
    private void DisableHomeCanvas() => homeCanvas.SetActive(false);
    private void EnableSettingsCanvas() => settingsCanvas.SetActive(true);
    private void QuitGame() => Application.Quit();

    private IEnumerator SelectDefaultItemCoroutine()
    {
        yield return null;
        EventSystem.current.SetSelectedGameObject(defaultSelection);
    }
}
