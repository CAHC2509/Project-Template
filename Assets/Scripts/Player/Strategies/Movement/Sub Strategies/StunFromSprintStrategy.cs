using UnityEngine;

public class StunFromSprintStrategy : MovementStrategyBase
{
    private float stunTimer;

    public override void Enter(PlayerMovementController player)
    {
        animationName = Constants.PlayerAnimations.STUN_FROM_SPRINT;
        base.Enter(player);

        player.SetVelocity(Vector2.zero);
        Vector2 force = new Vector2(player.Data.SprintStunForce.x * -player.FacingDirection, player.Data.SprintStunForce.y);
        player.AddForce(force, ForceMode2D.Impulse);

        stunTimer = 0f;
    }

    public override void Update()
    {
        base.Update();

        stunTimer += Time.deltaTime;

        if (stunTimer >= player.Data.SprintStunDuration)
            player.SetStrategy(player.Strategies.Idle);
    }
}
