using UnityEngine;

public class UmbrellaStrategy : InAirStrategy
{
    private float stunTimer;

    public override void Enter(PlayerMovementController player)
    {
        animationName = Constants.PlayerAnimations.UMBRELLA_INFLATE;
        base.Enter(player);

        player.SetVelocityY(0f);
        stunTimer = 0f;
    }

    public override void Update()
    {
        base.Update();

        HandleStunTimer();

        if (player.IsGrounded)
            TransitionToLand();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (player.IsTouchingHardSurface)
            ApplyForceInOppositeDirection();

        LimitFallSpeed();
    }

    protected override void AddListeners()
    {
        base.AddListeners();

        player.Input.OnHorizontalInput += OnHorizontalInput;
        player.Input.OnDashlnputPressed += OnDashInputPressed;
        player.Input.OnJumplnputCanceled += OnJumpInputCancelled;
    }

    protected override void RemoveListeners()
    {
        base.RemoveListeners();

        player.Input.OnHorizontalInput -= OnHorizontalInput;
        player.Input.OnDashlnputPressed -= OnDashInputPressed;
        player.Input.OnJumplnputCanceled -= OnJumpInputCancelled;
    }

    private void OnHorizontalInput(float input)
    {
        if (input != 0f && input != player.FacingDirection && airMovementEnabled)
            player.View.SetAnimation(Constants.PlayerAnimations.UMBRELLA_TURN);
    }

    private void OnDashInputPressed()
    {
        if (player.CanDash)
            player.SetStrategy(player.Strategies.Dash);
    }

    private void OnJumpInputCancelled()
    {
        (player.Strategies.Fall as MovementStrategyBase).SetEntryAnimation(Constants.PlayerAnimations.UMBRELLA_TO_FALL);
        player.SetStrategy(player.Strategies.Fall);
    }

    private void TransitionToLand()
    {
        (player.Strategies.Idle as MovementStrategyBase).SetEntryAnimation(Constants.PlayerAnimations.UMBRELLA_TO_IDLE);
        player.SetStrategy(player.Strategies.Idle);
    }

    private void HandleStunTimer()
    {
        if (!airMovementEnabled && stunTimer != 0f)
        {
            stunTimer -= Time.deltaTime;

            if (stunTimer <= 0f)
            {
                airMovementEnabled = true;
                stunTimer = 0f;
            }
        }
    }

    private void ApplyForceInOppositeDirection()
    {
        player.SetVelocityX(0f);
        Vector2 force = new Vector2(player.Data.UmbrellaCollissionForce * -player.FacingDirection, 0f);
        player.AddForce(force, ForceMode2D.Impulse);

        airMovementEnabled = false;
        stunTimer = player.Data.UmbrellaCollisionStunDuration;
    }

    private void LimitFallSpeed()
    {
        if (player.CurrentVelocity.y <= player.Data.UmbrellaMaxFallSpeed)
            player.SetVelocityY(-player.Data.UmbrellaMaxFallSpeed);
    }
}
