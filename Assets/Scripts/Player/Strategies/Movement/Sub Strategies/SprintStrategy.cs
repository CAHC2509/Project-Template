using UnityEngine;

public class SprintStrategy : GroundedStrategy
{
    private float accelerationTimer;

    public override void Enter(PlayerMovementController player)
    {
        animationName = Constants.PlayerAnimations.SPRINT;
        base.Enter(player);

        accelerationTimer = 1f;
    }

    public override void Update()
    {
        base.Update();

        HandleCollisions();
        HandleMidAirTransition();
        HandleDirectionChange();

        if (!player.Input.DashPressed)
        {
            (player.Strategies.Run as MovementStrategyBase).SetEntryAnimation(Constants.PlayerAnimations.DASH_TO_RUN);
            player.SetStrategy(player.Strategies.Run);
            return;
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        ApplySprintMovement();
    }

    protected override void AddListeners()
    {
        base.AddListeners();
        player.Input.OnJumplnputPressed += OnJumpInputPressed;
    }

    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        player.Input.OnJumplnputPressed -= OnJumpInputPressed;
    }

    private void HandleCollisions()
    {
        if (player.IsTouchingHardSurface)
            player.SetStrategy(player.Strategies.StunFromSprint);
    }

    private void HandleMidAirTransition()
    {
        if (!player.IsGrounded)
        {
            float finalVelocity = player.Data.AirSpeed * player.Data.LongFallAirSpeedMultiplier * player.FacingDirection;
            (player.Strategies.FallFromLongJump as FallFromLongJumpStrategy).SetEntryVelocityX(finalVelocity);
            player.SetStrategy(player.Strategies.FallFromLongJump);
        }
    }

    private void HandleDirectionChange()
    {
        float input = player.Input.HorizontalInput;
        if (input == 0f) return;

        int inputDirection = Mathf.RoundToInt(Mathf.Sign(input));

        if (inputDirection != player.FacingDirection)
        {
            player.View.SetAnimation(Constants.PlayerAnimations.SPRINTING_TURN);
            player.Flip(input);
            accelerationTimer = 0f;
        }
    }

    private void ApplySprintMovement()
    {
        if (accelerationTimer < 1f)
        {
            accelerationTimer += Time.fixedDeltaTime / player.Data.RunAccelerationTime;
            accelerationTimer = Mathf.Clamp01(accelerationTimer);
        }

        float curveValue = player.Data.SprintAccelerationCurve.Evaluate(accelerationTimer);
        float velocityX = curveValue * player.Data.SprintSpeed * player.FacingDirection;

        player.SetVelocityX(velocityX);
    }

    private void OnJumpInputPressed()
    {
        player.SetStrategy(player.Strategies.LongJump);
    }
}
