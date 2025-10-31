using UnityEngine;

public class AirDashStrategy : DashBaseStrategy
{
    public override void Enter(PlayerMovementController player)
    {
        animationName = Constants.PlayerAnimations.AIR_DASH;
        collissionData = player.CollissionProfiles.AirDash;
        base.Enter(player);
    }

    public override void Update()
    {
        base.Update();

        if (player.IsTouchingWall && dashTimer >= movementData.MinDashDuration)
        {
            player.SetStrategy(player.Strategies.WallGrab);
            return;
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (dashTimer <= movementData.DashDuration)
            player.SetVelocity(new Vector2(movementData.DashSpeed * player.FacingDirection, 0f));
    }

    protected override void HandleDashEnd()
    {
        TransitionToFallFromLongJump();
    }
}
