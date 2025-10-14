using UnityEngine;

public class DashStrategy : MovementStrategyBase
{
    private float dashTimer;

    public override void Enter(PlayerMovementController player)
    {
        animationName = Constants.Player.DASH_ANIMATION;
        base.Enter(player);

        dashTimer = 0f;
        player.ConsumeDash();
    }

    public override void Update()
    {
        base.Update();

        dashTimer += Time.deltaTime;

        if (dashTimer > player.Data.DashDuration)
        {
            if (player.Input.DashPressed && player.Input.HorizontalInput != 0f)
            {
                if (player.IsGrounded)
                    player.SetStrategy(player.Strategies.Sprint);
                else
                    player.SetStrategy(player.Strategies.FallFromLongJump);

                return;
            }
            else
            {
                if (player.IsGrounded)
                    player.SetStrategy(player.Strategies.Idle);
                else
                    player.SetStrategy(player.Strategies.Fall);
            }
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (dashTimer <= player.Data.DashDuration)
            player.SetVelocity(new Vector2(player.Data.DashSpeed * player.FacingDirection, 0f));
    }
}
