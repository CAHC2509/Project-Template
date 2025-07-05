using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsSubMenuBase : MonoBehaviour
{
    [SerializeField] private Button assignedButton;
    [SerializeField] private GameObject assignedPanel;

    public static event Action<GameObject> OnSubMenuRequested;
    public static event Action<Button> OnSubMenuButtonClicked;

    protected virtual void Awake() => assignedPanel.SetActive(false);

    protected virtual void OnEnable()
    {
        assignedButton?.onClick.AddListener(ShowSubMenuPanel);
        assignedButton?.onClick.AddListener(SubMenuButtonClicked);
    }

    protected virtual void OnDisable()
    {
        assignedButton?.onClick.RemoveListener(ShowSubMenuPanel);
        assignedButton?.onClick.RemoveListener(SubMenuButtonClicked);
    }

    protected virtual void ShowSubMenuPanel() => OnSubMenuRequested?.Invoke(assignedPanel);

    protected virtual void SubMenuButtonClicked() => OnSubMenuButtonClicked?.Invoke(assignedButton);
}
