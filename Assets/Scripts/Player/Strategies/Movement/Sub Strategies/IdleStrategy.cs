using UnityEngine;

public class IdleStrategy : GroundedStrategy
{
    public override void Enter(PlayerMovementController player)
    {
        animationName = Constants.PlayerAnimations.IDLE;
        collissionData = player.CollissionProfiles.Idle;
        base.Enter(player);
    }

    public override void Update()
    {
        base.Update();

        if (player.Input.HorizontalInput != 0f)
        {
            (player.Strategies.Run as MovementStrategyBase).SetEntryAnimation(Constants.PlayerAnimations.IDLE_TO_RUN);
            player.SetStrategy(player.Strategies.Run);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        player.SetVelocityX(0f);
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
}
