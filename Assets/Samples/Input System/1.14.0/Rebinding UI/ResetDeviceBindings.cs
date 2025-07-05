using UnityEngine;
using UnityEngine.InputSystem;

public class ResetDeviceBindings : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;
    [SerializeField] private ControlSchemeType targetControlScheme;

    public void ResetAllBindings()
    {
        foreach (InputActionMap map in inputActions.actionMaps)
            map.RemoveAllBindingOverrides();
    }

    public void ResetSpecificBindings()
    {
        string bindingGroup = ControlSchemeUtils.ToControlSchemeString(targetControlScheme);

        foreach (InputActionMap map in inputActions.actionMaps)
        {
            foreach (InputAction action in map.actions)
                action.RemoveBindingOverride(InputBinding.MaskByGroup(bindingGroup));
        }
    }
}

public enum ControlSchemeType
{
    KeyboardAndMouse,
    Gamepad,
    Touch,
    Joystick,
    XR
}

public static class ControlSchemeUtils
{
    public static string ToControlSchemeString(this ControlSchemeType scheme)
    {
        return scheme switch
        {
            ControlSchemeType.KeyboardAndMouse => "Keyboard&Mouse",
            ControlSchemeType.Gamepad => "Gamepad",
            ControlSchemeType.Touch => "Touch",
            ControlSchemeType.Joystick => "Joystick",
            ControlSchemeType.XR => "XR",
            _ => string.Empty,
        };
    }
}
