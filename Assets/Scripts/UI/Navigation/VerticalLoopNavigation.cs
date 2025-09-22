using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class VerticalLoopNavigation : MonoBehaviour
{
    private void Awake()
    {
        SetupNavigation();
    }

    private void SetupNavigation()
    {
        List<Selectable> buttons = new List<Selectable>();

        foreach (Transform child in transform)
        {
            if (child.TryGetComponent(out Selectable selectable))
                buttons.Add(selectable);
        }

        for (int i = 0; i < buttons.Count; i++)
        {
            Navigation nav = buttons[i].navigation;

            if (nav.mode == Navigation.Mode.Explicit)
                continue;

            nav.mode = Navigation.Mode.Explicit;

            int upIndex = (i - 1 + buttons.Count) % buttons.Count;
            int downIndex = (i + 1) % buttons.Count;

            nav.selectOnUp = buttons[upIndex];
            nav.selectOnDown = buttons[downIndex];

            nav.selectOnLeft = null;
            nav.selectOnRight = null;

            buttons[i].navigation = nav;
        }
    }
}
