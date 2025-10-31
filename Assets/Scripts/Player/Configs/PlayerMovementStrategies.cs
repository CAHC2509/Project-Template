using UnityEngine;

public class PlayerMovementStrategies
{
    public IMovementStrategy Idle { get; private set; }
    public IMovementStrategy Run { get; private set; }
    public IMovementStrategy Jump { get; private set; }
    public IMovementStrategy Fall { get; private set; }
    public IMovementStrategy LongJump { get; private set; }
    public IMovementStrategy FallFromLongJump { get; private set; }
    public IMovementStrategy ExtraJump { get; private set; }
    public IMovementStrategy WallGrab { get; private set; }
    public IMovementStrategy WallSlide { get; private set; }
    public IMovementStrategy WallRun { get; private set; }
    public IMovementStrategy WallJump { get; private set; }
    public IMovementStrategy GroundDash { get; private set; }
    public IMovementStrategy AirDash { get; private set; }
    public IMovementStrategy Sprint { get; private set; }
    public IMovementStrategy StunFromSprint { get; private set; }
    public IMovementStrategy LedgeClimb { get; private set; }
    public IMovementStrategy Umbrella { get; private set; }

    public PlayerMovementStrategies()
    {
        Idle = new IdleStrategy();
        Run = new RunStrategy();
        Jump = new JumpStrategy();
        Fall = new FallStrategy();
        LongJump = new LongJumpStrategy();
        FallFromLongJump = new FallFromLongJumpStrategy();
        ExtraJump = new ExtraJumpStrategy();
        WallGrab = new WallGrabStrategy();
        WallSlide = new WallSlideStrategy();
        WallRun = new WallRunStrategy();
        WallJump = new WallJumpStrategy();
        AirDash = new AirDashStrategy();
        GroundDash = new GroundDashStrategy();
        Sprint = new SprintStrategy();
        StunFromSprint = new StunFromSprintStrategy();
        LedgeClimb = new LedgeClimbStrategy();
        Umbrella = new UmbrellaStrategy();
    }
}
