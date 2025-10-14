using UnityEngine;

public class ExtraJumpStrategy : MovementStrategyBase
{
    private float elapsedTime;

    public override void Enter(PlayerMovementController player)
    {
        animationName = Constants.Player.LEDGE_CLIMB_ANIMATION;
        base.Enter(player);

        player.SetVelocity(Vector2.zero);
        Vector2 force = new Vector2(player.Data.ExtraJumpForce.x * player.Input.HorizontalInput, player.Data.ExtraJumpForce.y);
        player.AddForce(force, ForceMode2D.Impulse);
        player.ConsumeExtraJump();
        elapsedTime = 0f;
    }

    public override void Update()
    {
        base.Update();

        elapsedTime += Time.deltaTime;

        if (elapsedTime >= player.Data.ExtraJumpDuration)
            player.SetStrategy(player.Strategies.Fall);
    }
}
