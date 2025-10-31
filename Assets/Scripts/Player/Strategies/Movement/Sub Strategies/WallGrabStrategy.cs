using UnityEngine;

[System.Serializable]
public class WallGrabStrategy : WallContactStrategy
{
    private float currentDuration;

    public override void Enter(PlayerMovementController player)
    {
        animationName = Constants.PlayerAnimations.WALL_GRAB;
        base.Enter(player);

        currentDuration = 0f;

        player.SetVelocity(Vector2.zero);
        player.ResetExtraJump();
        player.ResetDash();
    }

    public override void Update()
    {
        base.Update();

        currentDuration += Time.deltaTime;

        if (currentDuration >= movementData.WallGrabDuration)
        {
            player.SetStrategy(player.Strategies.WallSlide);
            return;
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        player.SetVelocityY(0f);
    }
}
