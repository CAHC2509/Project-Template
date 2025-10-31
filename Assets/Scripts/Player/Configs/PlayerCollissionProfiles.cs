using System;

[Serializable]
public class PlayerCollissionProfiles
{
    public PlayerCollissionData Idle;
    public PlayerCollissionData Run;
    public PlayerCollissionData Jump;
    public PlayerCollissionData Fall;
    public PlayerCollissionData ExtraJump;
    public PlayerCollissionData WallGrab;
    public PlayerCollissionData WallSlide;
    public PlayerCollissionData WallRun;
    public PlayerCollissionData WallJump;
    public PlayerCollissionData GroundDash;
    public PlayerCollissionData AirDash;
    public PlayerCollissionData Sprint;
    public PlayerCollissionData StunFromSprint;
    public PlayerCollissionData LedgeClimb;
    public PlayerCollissionData Umbrella;
}
