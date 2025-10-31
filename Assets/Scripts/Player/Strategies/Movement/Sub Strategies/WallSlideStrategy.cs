using UnityEngine;

public class WallSlideStrategy : WallContactStrategy
{
    public override void Enter(PlayerMovementController player)
    {
        animationName = Constants.PlayerAnimations.WALL_SLIDE;
        base.Enter(player);

        player.SetVelocity(Vector2.zero);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        player.SetVelocityX(0f);
        
        if (player.CurrentVelocity.y <= -movementData.WallSlideMaxSpeed)
            player.SetVelocityY(-movementData.WallSlideMaxSpeed);
    }
}
