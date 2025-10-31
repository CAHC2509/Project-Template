using UnityEngine;

public class ExtraJumpStrategy : InAirStrategy
{
    private float defaultGravityScale;

    public override void Enter(PlayerMovementController player)
    {
        animationName = Constants.PlayerAnimations.EXTRA_JUMP;
        collissionData = player.CollissionProfiles.ExtraJump;
        base.Enter(player);

        defaultGravityScale = player.Rigidbody.gravityScale;
        player.Rigidbody.gravityScale = movementData.InAirGravityScale;

        player.SetVelocityY(0f);
        Vector2 force = new Vector2(player.CurrentVelocity.x, movementData.ExtraJumpForce);
        player.AddForce(force, ForceMode2D.Impulse);
        player.ConsumeExtraJump();
    }

    public override void Update()
    {
        base.Update();

        if (player.CurrentVelocity.y <= Constants.Physics.MIN_FALL_VELOCITY)
        {
            (player.Strategies.Fall as MovementStrategyBase).SetEntryAnimation(Constants.PlayerAnimations.ROLLING_FALL);
            player.SetStrategy(player.Strategies.Fall);
            return;
        }
    }

    public override void Exit()
    {
        base.Exit();

        player.Rigidbody.gravityScale = defaultGravityScale;
    }
}
