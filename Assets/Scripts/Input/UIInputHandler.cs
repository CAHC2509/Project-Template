using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIInputHandler : MonoBehaviour
{
    [SerializeField] private InputActionReference navigateInput;
    [SerializeField] private InputActionReference cancelInput;

    public static event Action OnLeft;
    public static event Action OnRight;
    public static event Action OnCancel;

    private void OnEnable()
    {
        navigateInput.action.performed += OnNavigate;
        cancelInput.action.performed += OnCancelInput;
    }

    private void OnDisable()
    {
        navigateInput.action.performed -= OnNavigate;
        cancelInput.action.performed -= OnCancelInput;
    }

    private void OnNavigate(InputAction.CallbackContext callbackContext)
    {
        Vector2 navigateInput = callbackContext.action.ReadValue<Vector2>();

        if (navigateInput.x < -0.5f)
            OnLeft?.Invoke();
        else if (navigateInput.x > 0.5f)
            OnRight?.Invoke();
    }

    private void OnCancelInput(InputAction.CallbackContext callbackContext) => OnCancel?.Invoke();
}
