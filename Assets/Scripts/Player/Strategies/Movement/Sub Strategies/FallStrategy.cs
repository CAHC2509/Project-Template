using UnityEngine;

public class FallStrategy : InAirStrategy
{
    private float currentFallMultiplier;
    private float lastTimeJumpPressed;
    private float entryVelocityX;
    private float startTime;
    private bool coyoteTimeExpired;
    private bool jumpByFallConsumed;

    public override void Enter(PlayerMovementController player)
    {
        animationName = Constants.PlayerAnimations.FALL_ENTRY;
        base.Enter(player);

        ApplyFallSettings();

        if (entryVelocityX != 0f)
            player.SetVelocityX(entryVelocityX);

        startTime = Time.time;
        coyoteTimeExpired = false;
        jumpByFallConsumed = false;
    }

    public override void Update()
    {
        base.Update();

        HandleBufferedJump();
        HandleWallSlide();
        HandleGroundTransitions();
        HandleCoyoteTime();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        ApplyFallAcceleration();
        LimitFallVelocity();
    }

    public override void Exit()
    {
        base.Exit();

        player.Rigidbody.gravityScale = player.Data.DefaultGravityScale;
        currentFallMultiplier = 0f;
        entryVelocityX = 0f;
    }

    protected override void AddListeners()
    {
        player.Input.OnHorizontalInput += OnHorizontalInput;
        player.Input.OnJumplnputPressed += OnJumpInputPressed;
        player.Input.OnDashlnputPressed += OnDashInputPressed;
    }

    protected override void RemoveListeners()
    {
        player.Input.OnHorizontalInput -= OnHorizontalInput;
        player.Input.OnJumplnputPressed -= OnJumpInputPressed;
        player.Input.OnDashlnputPressed -= OnDashInputPressed;
    }

    private void HandleBufferedJump()
    {
        bool bufferedJump = Time.time - lastTimeJumpPressed <= player.Data.JumpBuffer;

        if (player.IsGrounded && bufferedJump)
        {
            player.ResetJump();
            player.ResetExtraJump();
            player.ResetDash();
            player.SetStrategy(player.Strategies.Jump);
        }
    }

    private void HandleWallSlide()
    {
        if (player.Input.HorizontalInput == 0f) return;
        if (player.IsGrounded) return;
        if (!player.IsTouchingWall) return;

        player.SetStrategy(player.Strategies.WallGrab);
    }

    private void HandleGroundTransitions()
    {
        if (!player.IsGrounded) return;

        bool movingHorizontally = player.Input.HorizontalInput != 0f;
        bool isShortJump = currentFallMultiplier == player.Data.ShortFallMultiplier;

        if (movingHorizontally)
        {
            (player.Strategies.Run as MovementStrategyBase).SetEntryAnimation(Constants.PlayerAnimations.LAND_TO_RUN);
            player.SetStrategy(player.Strategies.Run);
        }
        else
        {
            string landAnim = isShortJump ? Constants.PlayerAnimations.HOP_LAND : Constants.PlayerAnimations.LAND;
            (player.Strategies.Idle as MovementStrategyBase).SetEntryAnimation(landAnim);
            player.SetStrategy(player.Strategies.Idle);
        }
    }

    private void HandleCoyoteTime()
    {
        if (jumpByFallConsumed) return;

        coyoteTimeExpired = Time.time - startTime > player.Data.CoyoteTime;
        if (coyoteTimeExpired)
        {
            player.ConsumeJump();
            jumpByFallConsumed = true;
        }
    }

    protected virtual void ApplyFallSettings()
    {
        airSpeed = player.Data.AirSpeed;

        if (currentFallMultiplier == 0f)
            currentFallMultiplier = player.Data.FallMultiplier;

        player.Rigidbody.gravityScale = player.Data.InAirGravityScale * currentFallMultiplier;
    }

    private void OnHorizontalInput(float input)
    {
        airMovementEnabled = true;
    }

    private void OnJumpInputPressed()
    {
        lastTimeJumpPressed = Time.time;

        if (player.CanJump && !coyoteTimeExpired)
        {
            player.SetStrategy(player.Strategies.Jump);
            return;
        }

        if (player.CanUseExtraJump && coyoteTimeExpired)
        {
            player.SetStrategy(player.Strategies.ExtraJump);
            return;
        }

        if (!player.CanUseExtraJump)
        {
            player.SetStrategy(player.Strategies.Umbrella);
            return;
        }
    }

    private void OnDashInputPressed()
    {
        if (player.CanDash)
            player.SetStrategy(player.Strategies.Dash);
    }

    private void ApplyFallAcceleration()
    {
        player.AddForce(Vector2.down * player.Data.FallAcceleration, ForceMode2D.Force);
    }

    private void LimitFallVelocity()
    {
        if (player.CurrentVelocity.y < -player.Data.MaxFallSpeed)
            player.SetVelocityY(-player.Data.MaxFallSpeed);
    }

    public void SetFallMultiplier(float multiplier)
    {
        currentFallMultiplier = multiplier;
    }

    public void SetEntryVelocityX(float velocityX)
    {
        airMovementEnabled = false;
        entryVelocityX = velocityX;
    }
}