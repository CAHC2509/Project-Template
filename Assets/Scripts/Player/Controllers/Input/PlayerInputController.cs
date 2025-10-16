using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : ControllerBase, IPlayerInputController
{
    [Header("Input action map")]
    [SerializeField] private InputActionAsset inputActions;

    [Space, Header("Movement input")]
    [SerializeField] private InputActionReference upInput;
    [SerializeField] private InputActionReference downInput;
    [SerializeField] private InputActionReference leftInput;
    [SerializeField] private InputActionReference rightInput;
    [SerializeField] private InputActionReference jumpInput;
    [SerializeField] private InputActionReference dashInput;

    private InputAction horizontalInput;
    private InputAction verticalInput;

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
        UpdateMovementBindings();
        inputActions.Enable();
        horizontalInput.Enable();
        verticalInput.Enable();

        base.Initialize();
    }

    public override void Conclude()
    {
        base.Conclude();

        inputActions.Disable();
        horizontalInput.Disable();
        verticalInput.Disable();
    }

    protected override void AddListeners()
    {
        horizontalInput.performed += HandleHorizontalInput;
        horizontalInput.canceled += HandleHorizontalInput;

        verticalInput.performed += HandleVerticalInput;
        verticalInput.canceled += HandleVerticalInput;

        jumpInput.action.performed += HandleJumpInput;
        jumpInput.action.canceled += HandleJumpInput;

        dashInput.action.performed += HandleDashInput;
        dashInput.action.canceled += HandleDashInput;

        IInputSettingsController.OnInputsRebinded += UpdateMovementBindings;
    }

    protected override void RemoveListeners()
    {
        horizontalInput.performed -= HandleHorizontalInput;
        horizontalInput.canceled -= HandleHorizontalInput;

        verticalInput.performed -= HandleVerticalInput;
        verticalInput.canceled -= HandleVerticalInput;

        jumpInput.action.performed -= HandleJumpInput;
        jumpInput.action.canceled -= HandleJumpInput;

        dashInput.action.performed -= HandleDashInput;
        dashInput.action.canceled -= HandleDashInput;

        IInputSettingsController.OnInputsRebinded -= UpdateMovementBindings;
    }

    private void HandleHorizontalInput(InputAction.CallbackContext context)
    {
        HorizontalInput = context.action.ReadValue<float>();

        if (HorizontalInput != 0f)
            OnHorizontalInput?.Invoke(HorizontalInput);
        else
            OnHorizontalInputCanceled?.Invoke();
    }

    private void HandleVerticalInput(InputAction.CallbackContext context)
    {
        VerticalInput = context.action.ReadValue<float>();

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

    public void UpdateMovementBindings()
    {
        horizontalInput = new InputAction("Horizontal", InputActionType.Value, expectedControlType: "Axis");
        horizontalInput.AddCompositeBinding("1DAxis")
            .With("Negative", leftInput.action.bindings[0].effectivePath)
            .With("Positive", rightInput.action.bindings[0].effectivePath);

        verticalInput = new InputAction("Vertical", InputActionType.Value, expectedControlType: "Axis");
        verticalInput.AddCompositeBinding("1DAxis")
            .With("Negative", downInput.action.bindings[0].effectivePath)
            .With("Positive", upInput.action.bindings[0].effectivePath);
    }
}
