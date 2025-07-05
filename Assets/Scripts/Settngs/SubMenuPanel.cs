using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class SubMenuPanel : MonoBehaviour
{
    [SerializeField] private GameObject defaultSelection;
    [SerializeField] private Transform contentPanel;

    private void OnEnable()
    {
        SetNavigation();
        StartCoroutine(SelectDefaultItem());
    }

    private void SetNavigation()
    {
        var buttons = contentPanel.transform.Cast<Transform>()
            .Select(t => t.GetComponent<Button>())
            .Where(b => b != null)
            .ToList();

        if (buttons.Count >= 2)
        {
            Button firstButton = buttons[0];
            Button secondButton = buttons[1];
            Button secondLastButton = buttons[buttons.Count - 2];
            Button lastButton = buttons[buttons.Count - 1];

            var firstNav = firstButton.navigation;
            firstNav.mode = Navigation.Mode.Explicit;
            firstNav.selectOnUp = lastButton;
            firstNav.selectOnDown = secondButton;
            firstButton.navigation = firstNav;

            var lastNav = lastButton.navigation;
            lastNav.mode = Navigation.Mode.Explicit;
            lastNav.selectOnUp = secondLastButton;
            lastNav.selectOnDown = firstButton;
            lastButton.navigation = lastNav;
        }
        else if (buttons.Count == 1)
        {
            var nav = buttons[0].navigation;
            nav.mode = Navigation.Mode.None;
            buttons[0].navigation = nav;
        }
    }

    private IEnumerator SelectDefaultItem()
    {
        yield return null;
        EventSystem.current.SetSelectedGameObject(defaultSelection);
    }
}
