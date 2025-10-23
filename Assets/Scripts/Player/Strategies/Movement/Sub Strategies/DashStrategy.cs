using UnityEngine;

public class DashStrategy : MovementStrategyBase
{
    private float dashTimer;

    public override void Enter(PlayerMovementController player)
    {
        animationName = player.IsGrounded ? Constants.PlayerAnimations.DASH : Constants.PlayerAnimations.AIR_DASH;
        base.Enter(player);

        dashTimer = 0f;
        player.ConsumeDash();
    }

    public override void Update()
    {
        base.Update();
        dashTimer += Time.deltaTime;

        if (player.IsTouchingWall && dashTimer >= player.Data.MinDashDuration)
        {
            player.SetStrategy(player.Strategies.WallGrab);
            return;
        }

        if (dashTimer > player.Data.DashDuration)
        {
            if (player.Input.DashPressed)
                HandleDashContinuation();
            else
                HandleDashEnd();
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (dashTimer <= player.Data.DashDuration)
            player.SetVelocity(new Vector2(player.Data.DashSpeed * player.FacingDirection, 0f));
    }

    private void HandleDashContinuation()
    {
        if (player.IsGrounded)
        {
            if (!player.IsTouchingHardSurface)
            {
                player.SetStrategy(player.Strategies.Sprint);
            }
            else
            {
                (player.Strategies.Run as MovementStrategyBase).SetEntryAnimation(Constants.PlayerAnimations.DASH_TO_RUN);
                player.SetStrategy(player.Strategies.Run);
            }
        }
        else
        {
            float finalVelocity = player.Data.AirSpeed * player.Data.LongFallAirSpeedMultiplier * player.FacingDirection;
            (player.Strategies.FallFromLongJump as FallFromLongJumpStrategy).SetEntryVelocityX(finalVelocity);
            (player.Strategies.FallFromLongJump as MovementStrategyBase).SetEntryAnimation(Constants.PlayerAnimations.ROLLING_FALL);
            player.SetStrategy(player.Strategies.FallFromLongJump);
        }
    }

    private void HandleDashEnd()
    {
        if (player.IsGrounded)
        {
            (player.Strategies.Idle as MovementStrategyBase).SetEntryAnimation(Constants.PlayerAnimations.DASH_TO_IDLE);
            player.SetStrategy(player.Strategies.Idle);
        }
        else
        {
            float finalVelocity = player.Data.AirSpeed * player.Data.LongFallAirSpeedMultiplier * player.FacingDirection;
            (player.Strategies.Fall as FallStrategy).SetEntryVelocityX(finalVelocity);
            (player.Strategies.Fall as MovementStrategyBase).SetEntryAnimation(Constants.PlayerAnimations.ROLLING_FALL);
            player.SetStrategy(player.Strategies.Fall);
        }
    }
}
