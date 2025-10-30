using System;

public interface IPlayerInputController
{
    public event Action<float> OnHorizontalInput;
    public event Action<float> OnVerticalnput;
    public event Action OnHorizontalInputCanceled;
    public event Action OnVerticalnputCanceled;
    public event Action OnJumplnputPressed;
    public event Action OnJumplnputCanceled;
    public event Action OnDashlnputPressed;
    public event Action OnDashlnputCanceled;

    public float HorizontalInput { get; }
    public float VerticalInput { get; }
    public bool JumpPressed { get; }
    public bool DashPressed { get; }
}
