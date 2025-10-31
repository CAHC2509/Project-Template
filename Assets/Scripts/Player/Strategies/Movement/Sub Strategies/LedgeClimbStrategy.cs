using UnityEngine;

public class LedgeClimbStrategy : MovementStrategyBase
{
    private Vector2 startPosition;
    private Vector2 targetPosition;
    private bool isClimbing;
    private bool verticalPhase;

    public override void Enter(PlayerMovementController player)
    {
        animationName = Constants.PlayerAnimations.LEDGE_CLIMB;
        collissionData = player.CollissionProfiles.LedgeClimb;
        base.Enter(player);

        if (!player.CanGrabLedge)
        {
            player.SetStrategy(player.Strategies.Fall);
            return;
        }

        startPosition = player.transform.position;
        targetPosition = player.LedgePosition
                         + Vector2.up * movementData.LedgeClimbOffset.y
                         + Vector2.right * player.FacingDirection * movementData.LedgeClimbOffset.x;

        targetPositionVertical = new Vector2(startPosition.x, targetPosition.y);

        player.SetVelocity(Vector2.zero);
        player.View.SetAnimation(animationName);
        isClimbing = true;
        verticalPhase = true;
    }

    private Vector2 targetPositionVertical;

    public override void Update()
    {
        base.Update();

        if (!isClimbing) return;

        player.SetVelocity(Vector2.zero);

        if (verticalPhase)
        {
            player.transform.position = Vector2.MoveTowards(player.transform.position, targetPositionVertical, movementData.LedgeClimbSpeed * Time.deltaTime);

            if (Vector2.Distance(player.transform.position, targetPositionVertical) < 0.01f)
                verticalPhase = false;
        }
        else
        {
            Vector2 horizontalTarget = new Vector2(targetPosition.x, player.transform.position.y);
            player.transform.position = Vector2.MoveTowards(player.transform.position, horizontalTarget, movementData.LedgeClimbSpeed * Time.deltaTime);

            if (Vector2.Distance(player.transform.position, horizontalTarget) < 0.01f)
                EndClimb();
        }
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
        (player.Strategies.Jump as MovementStrategyBase).SetEntryAnimation(Constants.PlayerAnimations.MANTLE_TO_JUMP);
        player.SetStrategy(player.Strategies.Jump);
    }

    private void EndClimb()
    {
        isClimbing = false;
        player.transform.position = targetPosition;

        if (player.Input.HorizontalInput != 0f)
        {
            (player.Strategies.Run as MovementStrategyBase).SetEntryAnimation(Constants.PlayerAnimations.MANTLE_TO_RUN);
            player.SetStrategy(player.Strategies.Run);
        }
        else
        {
            (player.Strategies.Idle as MovementStrategyBase).SetEntryAnimation(Constants.PlayerAnimations.MANTLE_TO_IDLE);
            player.SetStrategy(player.Strategies.Idle);
        }
    }
}
