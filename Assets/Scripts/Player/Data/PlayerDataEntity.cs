using System;
using UnityEngine;

[Serializable]
public class PlayerDataEntity
{
    [Header("Run")]
    [SerializeField] private float runMaxSpeed = 5f;
    [SerializeField] private float runAccelerationTime = 0.5f;
    [SerializeField] private AnimationCurve runAccelerationCurve = AnimationCurve.EaseInOut(0f, 0.85f, 1f, 1f);

    [Header("Sprint")]
    [SerializeField] private AnimationCurve sprintAccelerationCurve = AnimationCurve.EaseInOut(0f, 0.85f, 1f, 1f);
    [SerializeField] private float sprintSpeed = 7.5f;
    [SerializeField] private float changeSprintDirectionTimer = 0.1f;
    [SerializeField] private float sprintStunDuration = 0.25f;
    [SerializeField] private Vector2 sprintStunForce = new Vector2(2.5f, 7.5f);

    [Header("Jump")]
    [SerializeField] private AnimationCurve jumpForceCurve = AnimationCurve.EaseInOut(0f, 0.85f, 1f, 0.5f);
    [SerializeField] private float initialJumpForce = 6.5f;
    [SerializeField] private float maxJumpForce = 25f;
    [SerializeField] private float maxJumpHoldTime = 0.3f;
    [SerializeField] private float coyoteTime = 0.3f;
    [SerializeField] private float jumpBuffer = 0.15f;
    [SerializeField] private bool hasExtraJump = true;

    [Header("Long jump")]
    [SerializeField] private float longJumpForceMultiplier = 1.5f;
    [SerializeField] private float maxLongJumpForceMultiplier = 1.5f;

    [Header("Fall from long jump")]
    [SerializeField] private float longFallAirSpeedMultiplier = 1.5f;

    [Header("Extra jump")]
    [SerializeField] private Vector2 extraJumpForce = new Vector2(6.5f, 12.5f);
    [SerializeField] private float extraJumpDuration = 0.5f;

    [Header("Air movement")]
    [SerializeField] private float airSpeed = 5f;
    [SerializeField] private float airAccelerationSpeed = 0.15f;

    [Header("Falling")]
    [SerializeField] private float fallAcceleration = 30f;
    [SerializeField] private float maxFallSpeed = 20f;

    [Header("Wall slide")]
    [SerializeField] private float wallSlideSpeed = 5f;

    [Header("Wall jump")]
    [SerializeField] private Vector2 wallJumpForce = new Vector2(5f, 5f);
    [SerializeField] private float wallJumpDuration = 0.2f;

    [Header("Dash")]
    [SerializeField] private float dashSpeed = 12.5f;
    [SerializeField] private float dashDuration = 0.75f;

    [Header("Ledge climb")]
    [SerializeField] private float ledgeClimbSpeed = 3f;
    [SerializeField] private Vector2 ledgeClimbOffset = new Vector2(0.5f, 1f);

    public float RunMaxSpeed => runMaxSpeed;
    public float RunAccelerationTime => runAccelerationTime;
    public AnimationCurve RunAccelerationCurve => runAccelerationCurve;

    public AnimationCurve SprintAccelerationCurve => sprintAccelerationCurve;
    public float SprintSpeed => sprintSpeed;
    public float ChangeSprintDirectionTimer => changeSprintDirectionTimer;
    public float SprintStunDuration => sprintStunDuration;
    public Vector2 SprintStunForce => sprintStunForce;

    public AnimationCurve JumpForceCurve => jumpForceCurve;
    public float InitialJumpForce => initialJumpForce;
    public float MaxJumpForce => maxJumpForce;
    public float MaxJumpHoldTime => maxJumpHoldTime;
    public float CoyoteTime => coyoteTime;
    public float JumpBuffer => jumpBuffer;
    public bool HasExtraJump => hasExtraJump;

    public float LongJumpForceMultiplier => longJumpForceMultiplier;
    public float MaxLongJumpForceMultiplier => maxLongJumpForceMultiplier;

    public float LongFallAirSpeedMultiplier => longFallAirSpeedMultiplier;

    public Vector2 ExtraJumpForce => extraJumpForce;
    public float ExtraJumpDuration => extraJumpDuration;

    public float AirSpeed => airSpeed;
    public float AirAccelerationSpeed => airAccelerationSpeed;

    public float FallAcceleration => fallAcceleration;
    public float MaxFallSpeed => maxFallSpeed;
    
    public float WallSlideSpeed => wallSlideSpeed;

    public Vector2 WallJumpForce => wallJumpForce;
    public float WallJumpDuration => wallJumpDuration;

    public float DashSpeed => dashSpeed;
    public float DashDuration => dashDuration;

    public float LedgeClimbSpeed => ledgeClimbSpeed;
    public Vector2 LedgeClimbOffset => ledgeClimbOffset;
}
