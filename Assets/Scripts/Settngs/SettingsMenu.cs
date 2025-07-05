using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject defaultSelection;
    [SerializeField] private List<Button> mainButtons;

    private GameObject currentSubMenuPanel;
    private GameObject lastSelectedSubMenuButton;

    public static event Action OnSettingsClose;

    private void OnEnable()
    {
        closeButton.onClick.AddListener(CloseAll);

        SettingsSubMenuBase.OnSubMenuRequested += ChangeSubMenuPanel;
        SettingsSubMenuBase.OnSubMenuButtonClicked += SaveLastSelectedButton;

        UIInputHandler.OnCancel += HandleCancel;

        StartCoroutine(SelectDefaultItem());
    }

    private void OnDisable()
    {
        closeButton.onClick.RemoveListener(CloseAll);

        SettingsSubMenuBase.OnSubMenuRequested -= ChangeSubMenuPanel;
        SettingsSubMenuBase.OnSubMenuButtonClicked -= SaveLastSelectedButton;

        UIInputHandler.OnCancel -= HandleCancel;
    }

    private void ChangeSubMenuPanel(GameObject newSubMenuPanel)
    {
        if (currentSubMenuPanel != null)
            currentSubMenuPanel.SetActive(false);

        currentSubMenuPanel = newSubMenuPanel;
        currentSubMenuPanel.SetActive(true);
    }

    private void SaveLastSelectedButton(Button selectedButton)
    {
        lastSelectedSubMenuButton = selectedButton.gameObject;
    }

    private void CloseCurrentSubMenuPanel()
    {
        EventSystem.current.SetSelectedGameObject(lastSelectedSubMenuButton);

        currentSubMenuPanel.SetActive(false);
        currentSubMenuPanel = null;
    }

    private void HandleCancel()
    {
        if (currentSubMenuPanel == null)
        {
            OnSettingsClose?.Invoke();
            settingsPanel.SetActive(false);
        }
        else
        {
            CloseCurrentSubMenuPanel();
        }
    }

    private void CloseAll()
    {
        if (currentSubMenuPanel != null)
            currentSubMenuPanel.SetActive(false);

        currentSubMenuPanel = null;
        OnSettingsClose?.Invoke();
        settingsPanel.SetActive(false);
    }

    private IEnumerator SelectDefaultItem()
    {
        yield return null;
        EventSystem.current.SetSelectedGameObject(defaultSelection);
        lastSelectedSubMenuButton = defaultSelection;
    }
}
