using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIInputManager : MonoBehaviour
{
    [SerializeField] private InputActionReference navigateInput;
    [SerializeField] private InputActionReference cancelInput;

    public static event Action OnLeft;
    public static event Action OnRight;
    public static event Action OnCancel;

    private void Awake()
    {
        navigateInput.action.performed += HandleNavigationInput;
        cancelInput.action.performed += CancelInput;
    }

    private void HandleNavigationInput(InputAction.CallbackContext callbackContext)
    {
        float horizontalInput = callbackContext.action.ReadValue<Vector2>().x;

        if (horizontalInput == 0f) return;

        if (horizontalInput > 0f)
            RightInput();
        else
            LeftInput();
    }

    private void CancelInput(InputAction.CallbackContext callbackContext) => OnCancel?.Invoke();

    private void LeftInput()
    {
        OnLeft?.Invoke();
    }

    private void RightInput()
    {
        OnRight?.Invoke();
    }
}
