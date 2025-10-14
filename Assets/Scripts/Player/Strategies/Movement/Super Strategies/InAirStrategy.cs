using UnityEngine;

public class InAirStrategy : MovementStrategyBase
{
    protected float airSpeedToUse;

    public override void Update()
    {
        base.Update();

        player.Flip(player.Input.HorizontalInput);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        MoveInAir();
    }

    protected void MoveInAir()
    {
        float targetSpeed = player.Input.HorizontalInput * airSpeedToUse;
        player.SetVelocityX(Mathf.Lerp(player.CurrentVelocity.x, targetSpeed, player.Data.AirAccelerationSpeed));
    }
}
