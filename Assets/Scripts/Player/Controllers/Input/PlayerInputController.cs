using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : ControllerBase, IPlayerInputController
{
    [Header("Input action map")]
    [SerializeField] private InputActionAsset inputActions;

    [Space, Header("Movement input")]
    [SerializeField] private InputActionReference horizontalInput;
    [SerializeField] private InputActionReference verticalInput;
    [SerializeField] private InputActionReference jumpInput;
    [SerializeField] private InputActionReference dashInput;

    public event Action<float> OnHorizontalInput;
    public event Action<float> OnVerticalnput;
    public event Action OnHorizontalInputCanceled;
    public event Action OnVerticalnputCanceled;
    public event Action OnJumplnputPressed;
    public event Action OnJumplnputCanceled;
    public event Action OnDashlnputPressed;
    public event Action OnDashlnputCanceled;

    public float HorizontalInput { get; private set; }
    public float VerticalInput { get; private set; }
    public bool JumpPressed { get; private set; }
    public bool DashPressed { get; private set; }

    public override void Initialize()
    {
        inputActions.Enable();

        base.Initialize();
    }

    public override void Conclude()
    {
        base.Conclude();

        inputActions.Disable();
    }

    protected override void AddListeners()
    {
        horizontalInput.action.performed += HandleHorizontalInput;
        horizontalInput.action.canceled += HandleHorizontalInput;

        verticalInput.action.performed += HandleVerticalInput;
        verticalInput.action.canceled += HandleVerticalInput;

        jumpInput.action.performed += HandleJumpInput;
        jumpInput.action.canceled += HandleJumpInput;

        dashInput.action.performed += HandleDashInput;
        dashInput.action.canceled += HandleDashInput;
    }

    protected override void RemoveListeners()
    {
        horizontalInput.action.performed -= HandleHorizontalInput;
        horizontalInput.action.canceled -= HandleHorizontalInput;

        verticalInput.action.performed -= HandleVerticalInput;
        verticalInput.action.canceled -= HandleVerticalInput;

        jumpInput.action.performed -= HandleJumpInput;
        jumpInput.action.canceled -= HandleJumpInput;

        dashInput.action.performed -= HandleDashInput;
        dashInput.action.canceled -= HandleDashInput;
    }

    private void HandleHorizontalInput(InputAction.CallbackContext context)
    {
        HorizontalInput = context.action.ReadValue<float>();
        HorizontalInput = SnapAxisInput(HorizontalInput);

        if (HorizontalInput != 0f)
            OnHorizontalInput?.Invoke(HorizontalInput);
        else
            OnHorizontalInputCanceled?.Invoke();
    }

    private void HandleVerticalInput(InputAction.CallbackContext context)
    {
        VerticalInput = context.action.ReadValue<float>();
        VerticalInput = SnapAxisInput(VerticalInput);

        if (VerticalInput != 0f)
            OnVerticalnput?.Invoke(VerticalInput);
        else
            OnVerticalnputCanceled?.Invoke();
    }

    private void HandleJumpInput(InputAction.CallbackContext context)
    {
        float inputValue = context.action.ReadValue<float>();

        if (inputValue != 0f)
            OnJumplnputPressed?.Invoke();
        else
            OnJumplnputCanceled?.Invoke();

        JumpPressed = inputValue != 0f;
    }

    private void HandleDashInput(InputAction.CallbackContext context)
    {
        float inputValue = context.action.ReadValue<float>();

        if (inputValue != 0f)
            OnDashlnputPressed?.Invoke();
        else
            OnDashlnputCanceled?.Invoke();

        DashPressed = inputValue != 0f;
    }

    private float SnapAxisInput(float input)
    {
        if (input == 0f) return 0f;

        if (input > 0) return 1f;
        else return -1f;
    }
}
