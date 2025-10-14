using UnityEngine;

public class FallStrategy : InAirStrategy
{
    private float lastTimeJumpPressed;
    private float startTime;
    private bool coyoteTimeExpired;
    private bool jumpByFallConsumed;

    public override void Enter(PlayerMovementController player)
    {
        animationName = Constants.Player.FALL_ANIMATION;
        base.Enter(player);

        ApplyFallSettings();

        startTime = Time.time;
        coyoteTimeExpired = false;
        jumpByFallConsumed = false;
    }

    public override void Update()
    {
        base.Update();

        if (player.IsGrounded && Time.time - lastTimeJumpPressed <= player.Data.JumpBuffer)
        {
            player.ResetJump();
            player.ResetExtraJump();
            player.ResetDash();
            player.SetStrategy(player.Strategies.Jump);
            return;
        }

        if (player.Input.HorizontalInput != 0f && player.CanGrabLedge)
        {
            player.SetStrategy(player.Strategies.LedgeClimb);
            return;
        }

        if (player.Input.HorizontalInput != 0f && player.IsTouchingWall && !player.IsGrounded)
        {
            player.SetStrategy(player.Strategies.WallSlide);
            return;
        }

        if (player.IsGrounded)
        {
            player.SetStrategy(player.Strategies.Idle);
            return;
        }

        coyoteTimeExpired = Time.time - startTime > player.Data.CoyoteTime;

        if (coyoteTimeExpired && !jumpByFallConsumed)
        {
            player.ConsumeJump();
            jumpByFallConsumed = true;
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        ApplyFallAcceleration();

        if (player.CurrentVelocity.y < -player.Data.MaxFallSpeed)
            LimitFallVelocity();
    }

    protected override void AddListeners()
    {
        player.Input.OnJumplnputPressed += OnJumpInputPressed;
    }

    protected override void RemoveListeners()
    {
        player.Input.OnJumplnputPressed -= OnJumpInputPressed;
    }

    protected virtual void ApplyFallSettings()
    {
        airSpeedToUse = player.Data.AirSpeed;
    }

    private void OnJumpInputPressed()
    {
        lastTimeJumpPressed = Time.time;

        if (player.CanJump && !coyoteTimeExpired)
            player.SetStrategy(player.Strategies.Jump);

        if (player.CanUseExtraJump && coyoteTimeExpired)
            player.SetStrategy(player.Strategies.ExtraJump);
    }

    private void ApplyFallAcceleration() => player.AddForce(Vector2.down * player.Data.FallAcceleration, ForceMode2D.Force);
    private void LimitFallVelocity() => player.SetVelocityY(-player.Data.MaxFallSpeed);
}