using UnityEngine;

public class RunStrategy : GroundedStrategy
{
    private float accelerationTimer;
    private float direction;

    public override void Enter(PlayerMovementController player)
    {
        animationName = Constants.Player.RUN_ANIMATION;
        base.Enter(player);
        accelerationTimer = 0f;
        direction = Mathf.Sign(player.Input.HorizontalInput);
    }

    public override void Update()
    {
        base.Update();

        player.Flip(player.Input.HorizontalInput);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (player.Input.HorizontalInput != 0)
        {
            direction = Mathf.Sign(player.Input.HorizontalInput);
            accelerationTimer += Time.fixedDeltaTime / player.Data.RunAccelerationTime;
            accelerationTimer = Mathf.Clamp01(accelerationTimer);
        }
        else
        {
            accelerationTimer = 0f;
        }

        float curveValue = player.Data.RunAccelerationCurve.Evaluate(accelerationTimer);
        float velocityX = curveValue * player.Data.RunMaxSpeed * player.Input.HorizontalInput;
        player.SetVelocityX(velocityX);
    }

    protected override void AddListeners()
    {
        base.AddListeners();
        player.Input.OnHorizontalInputCanceled += OnHorizontalInputCanceled;
        player.Input.OnJumplnputPressed += OnJumplnputPressed;
    }

    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        player.Input.OnHorizontalInputCanceled -= OnHorizontalInputCanceled;
        player.Input.OnJumplnputPressed -= OnJumplnputPressed;
    }

    private void OnHorizontalInputCanceled() => player.SetStrategy(player.Strategies.Idle);
    private void OnJumplnputPressed() => player.SetStrategy(player.Strategies.Jump);
}
