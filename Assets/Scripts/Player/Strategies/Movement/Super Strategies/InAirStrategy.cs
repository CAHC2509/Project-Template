using UnityEngine;

public abstract class InAirStrategy : MovementStrategyBase
{
    protected float airSpeed;
    protected bool airMovementEnabled = true;

    public override void Enter(PlayerMovementController player)
    {
        base.Enter(player);

        airSpeed = movementData.AirSpeed;
    }

    public override void Update()
    {
        base.Update();

        HandleLedgeClimb();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        MoveInAir();
    }

    public override void Exit()
    {
        base.Exit();

        airMovementEnabled = true;
        RemoveListeners();
    }

    protected void MoveInAir()
    {
        if (!airMovementEnabled) return;

        float input = player.Input.HorizontalInput;
        float targetSpeed = input * airSpeed;
        player.SetVelocityX(targetSpeed);

        if (input != 0f && input != player.FacingDirection)
            player.Flip(input);
    }

    protected void HandleLedgeClimb()
    {
        if (!player.CanGrabLedge) return;
        if (player.Input.HorizontalInput == 0f) return;
        if (player.Input.JumpPressed && !player.Input.DashPressed) return;

        player.SetStrategy(player.Strategies.LedgeClimb);
    }
}
