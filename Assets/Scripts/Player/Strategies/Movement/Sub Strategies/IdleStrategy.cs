using UnityEngine;

public class IdleStrategy : GroundedStrategy
{
    public override void Enter(PlayerMovementController player)
    {
        animationName = Constants.Player.IDLE_ANIMATION;

        base.Enter(player);
    }

    public override void Update()
    {
        base.Update();

        if (player.Input.HorizontalInput != 0f)
            player.SetStrategy(player.Strategies.Run);
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

    private void OnJumpInputPressed()
    {
        if (player.CanJump)
            player.SetStrategy(player.Strategies.Jump);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        player.SetVelocityX(0f);
    }
}
