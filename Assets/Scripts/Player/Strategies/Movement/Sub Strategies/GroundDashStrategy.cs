using UnityEngine;

public class GroundDashStrategy : DashBaseStrategy
{
    public override void Enter(PlayerMovementController player)
    {
        animationName = Constants.PlayerAnimations.DASH;
        collissionData = player.CollissionProfiles.GroundDash;
        base.Enter(player);

        SnapPlayerToGround();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (dashTimer <= movementData.DashDuration)
        {
            if (!player.IsGrounded)
                player.SetVelocity(new Vector2(movementData.DashSpeed * player.FacingDirection, player.CurrentVelocity.y));
            else
                player.SetVelocity(new Vector2(movementData.DashSpeed * player.FacingDirection, 0f));

            SnapPlayerToGround();
        }
    }

    protected override void HandleDashEnd()
    {
        if (player.Input.DashPressed)
        {
            if (player.IsGrounded)
            {
                if (!player.IsTouchingHardSurface)
                {
                    player.SetStrategy(player.Strategies.Sprint);
                }
                else
                {
                    (player.Strategies.Idle as MovementStrategyBase).SetEntryAnimation(Constants.PlayerAnimations.DASH_TO_IDLE);
                    player.SetStrategy(player.Strategies.Idle);
                }
            }
            else
            {
                TransitionToFallFromLongJump();
            }
        }
        else
        {
            if (player.IsGrounded)
            {
                if (player.Input.HorizontalInput != 0f)
                {
                    (player.Strategies.Run as MovementStrategyBase).SetEntryAnimation(Constants.PlayerAnimations.DASH_TO_RUN);
                    player.SetStrategy(player.Strategies.Run);
                }
                else
                {
                    (player.Strategies.Idle as MovementStrategyBase).SetEntryAnimation(Constants.PlayerAnimations.DASH_TO_IDLE);
                    player.SetStrategy(player.Strategies.Idle);
                }
            }
            else
            {
                player.SetStrategy(player.Strategies.Fall);
            }
        }
    }
}
