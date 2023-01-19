using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character Data")]
public class CharacterData : ScriptableObject
{
    [Header("Health")]
    public int maxHealth;
    [Header("Stamina")]
    public float maxStamina;
    public float regeneration;
    public float recoverRate;
    [HideInInspector] public float gravityStrenght, gravityScale;

    [Header("Gravity")]
    public float fallGravityMult;
    public float maxFallSpeed;

    public float fastFallGravityMult, maxFastFallSpeed;

    [Header("Run")]
    public float runMaxSpeed;
    public float runAcceleration;
    [HideInInspector] public float runAccelAmount;
    public float runDecceleration;
    [HideInInspector] public float runDeccelAmount;

    [Range(0f, 1f)] public float accelInAir, deccelInAir;

    public bool doConserveMomentum;

    [Header("Jump")]
    public int jumpQuantity;
    public float jumpHeight;
    public float jumpTimeToApex;
    [HideInInspector] public float jumpForce;

    [Header("Both Jumps")]
    public float jumpCutGravityMult;
    [Range(0f, 1f)] public float jumpHangGravityMult;
    public float jumpHangTimeThreshold;
    public float jumpHangAccelerationMult, jumpHangMaxSpeedMult;

    //[Header("Wall Jump")]
    //public Vector2 wallJumpForce;
    //[Range(0f, 1f)] public float wallJumpRunLerp;
    //[Range(0f, 1.5f)] public float wallJumpTime;
    //public bool doTurnOnWallJump;

    [Header("Slide")]
    public float slideSpeed;
    public float slideAccel;

    [Header("Assists")]
    [Range(0.01f, 0.5f)] public float coyoteTime;
    [Range(0.01f, 0.5f)]public float jumpInputBufferTime;

    [Header("Dash")]
    public int dashForce;
    public float dashISeconds;
    public float downwardForce;
    public float staminaCost;
    //public float dashEndTime;
    //public Vector2 dashEndSpeed;
    //[Range(0f, 1f)] public float dashEndRunLerp;
    //public float dashRefillTime;
    [Range(0.01f, 0.5f)] public float dashInputBufferTime;

    private void OnValidate()
    {
        gravityStrenght = -(2 * jumpHeight) / (jumpTimeToApex * jumpTimeToApex);
        gravityScale = gravityStrenght / Physics2D.gravity.y;

        runAccelAmount = (50 * runAcceleration) / runMaxSpeed;
        runDeccelAmount = (50 * runDecceleration) / runMaxSpeed;

        jumpForce = Mathf.Abs(gravityStrenght) * jumpTimeToApex;

        runAcceleration = Mathf.Clamp(runAcceleration, 0.01f, runMaxSpeed);
        runDecceleration = Mathf.Clamp(runDecceleration, 0.01f, runMaxSpeed);
    }
}
