using UnityEngine;

public class WallRunStrategy : WallContactStrategy
{
    private float currentDuration;

    public override void Enter(PlayerMovementController player)
    {
        animationName = Constants.PlayerAnimations.WALL_RUN;
        collissionData = player.CollissionProfiles.WallRun;
        base.Enter(player);

        currentDuration = 0f;

        player.SetVelocityY(0f);
    }

    public override void Update()
    {
        base.Update();

        currentDuration += Time.deltaTime;

        if (currentDuration >= movementData.WallRunDuration)
            player.SetStrategy(player.Strategies.WallGrab);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        player.SetVelocityY(movementData.WallRunSpeed);
    }
}
