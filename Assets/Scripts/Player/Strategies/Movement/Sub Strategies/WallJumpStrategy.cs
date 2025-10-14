using UnityEngine;

public class WallJumpStrategy : MovementStrategyBase
{
    private float elapsedTime;

    public override void Enter(PlayerMovementController player)
    {
        animationName = Constants.Player.JUMP_ANIMATION;
        base.Enter(player);

        player.SetVelocity(Vector2.zero);
        Vector2 force = new Vector2(player.Data.WallJumpForce.x * -player.FacingDirection, player.Data.WallJumpForce.y);
        player.AddForce(force, ForceMode2D.Impulse);
        player.Flip(-player.FacingDirection);
        elapsedTime = 0f;
    }
    public override void Update()
    {
        base.Update();

        elapsedTime += Time.deltaTime;

        if (elapsedTime >= player.Data.WallJumpDuration)
        {
            player.SetStrategy(player.Strategies.Jump);
            return;
        }
    }
}
