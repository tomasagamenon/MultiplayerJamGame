using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Movement : Stamina
{
    // Character movement values

    private Rigidbody2D _rb;
    private SpriteRenderer sprite;
    #region State Parameters
    // States
    protected bool isFacingRight;
    protected bool isJumping;
    protected bool isSliding;
    protected bool isDashing;
    protected bool isSlowed;
    // Timer
    protected float lastOnGroundTime;
    // Jump
    private bool _isJumpCut;
    private bool _isJumpFalling;
    private int _jumpQuantity;
    #endregion
    #region Input Parameters
    private Vector2 _moveInput;
    private float lastPressedJumpTime;
    private float lastPressedDashTime;
    #endregion

    #region Check Parameters
    [SerializeField] private Transform _groundCheckPoint;
    [SerializeField] private Vector2 _groundCheckSize = new(0.49f, 0.03f);
    #endregion

    [SerializeField] private LayerMask _groundLayer;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }
    new private void Start()
    {
        isFacingRight = true;
        sprite.flipX = false;
        SetGravityScale(Data.gravityScale);
        _jumpQuantity = Data.jumpQuantity;
    }
    public void OnMove(InputAction.CallbackContext input)
    {
        _moveInput = input.ReadValue<Vector2>();
        if (_moveInput.x != 0)
            CheckDirectionToFace(_moveInput.x > 0);
    }

    public void OnJump(InputAction.CallbackContext input)
    {
        if (input.performed)
            OnJumpInput();
        if (input.canceled)
            OnJumpUpInput();
    }
    public void OnDash(InputAction.CallbackContext input)
    {
        if(input.performed)
            OnDashInput();
        //Debug.Log("Dash imput");
    }

    private void Update()
    {
        #region Timers
        lastOnGroundTime -= Time.deltaTime;
        lastPressedJumpTime -= Time.deltaTime;
        lastPressedDashTime -= Time.deltaTime;

        #endregion

        #region Collision Checks
        if (!isJumping && !isDashing)
        {
            if (Physics2D.OverlapBox(_groundCheckPoint.position, _groundCheckSize, 0, _groundLayer))
            {
                lastOnGroundTime = Data.coyoteTime;
                _jumpQuantity = Data.jumpQuantity;
            }
            else if (lastOnGroundTime < 0 && _jumpQuantity != Data.jumpQuantity - 1)
            {
                _jumpQuantity--;
            }
        }
        #endregion
        #region Jump Checks
        if (isJumping && _rb.velocity.y < 0)
        {
            isJumping = false;
            _isJumpFalling = true;
        }

        if (lastOnGroundTime > 0 && !isJumping)
        {
            _isJumpCut = false;
            _isJumpFalling = false;
        }
        if (!isDashing)
        {
            if (CanJump() && lastPressedJumpTime > 0)
            {
                isJumping = true;
                _isJumpCut = false;
                _isJumpFalling = false;
                _jumpQuantity--;
                Jump();
            }
        }
        #endregion
        #region Dash Cheks
        if(lastPressedDashTime > 0 && CanDash())
        {
            Debug.Log("Activating dash");
            isDashing = true;
            Dash();
        }
        #endregion
        #region Slide Checks
        if (CanSlide())
            isSliding = true;
        else
            isSliding = false;
        #endregion
        #region Gravity
        if (isSliding)
            SetGravityScale(0);
        else if (_rb.velocity.y < 0 && _moveInput.y < 0)
        {
            SetGravityScale(Data.gravityScale * Data.fastFallGravityMult);
            _rb.velocity = new Vector2(_rb.velocity.x, Mathf.Max(_rb.velocity.y, -Data.maxFastFallSpeed));
        }
        else if (_isJumpCut)
        {
            SetGravityScale(Data.gravityScale * Data.jumpCutGravityMult);
            _rb.velocity = new Vector2(_rb.velocity.x, Mathf.Max(_rb.velocity.y, -Data.maxFallSpeed));
        }
        else if ((isJumping || _isJumpFalling) && Mathf.Abs(_rb.velocity.y) < Data.jumpHangTimeThreshold)
        {
            SetGravityScale(Data.gravityScale * Data.jumpHangGravityMult);
        }
        else if (_rb.velocity.y < 0)
        {
            SetGravityScale(Data.gravityScale * Data.fallGravityMult);
            _rb.velocity = new Vector2(_rb.velocity.x, Mathf.Max(_rb.velocity.y, -Data.maxFallSpeed));
        }
        else
            SetGravityScale(Data.gravityScale);
        #endregion
    }
    private void FixedUpdate()
    {
        if(!isDashing)
            Move();
        if (isSliding)
        {
            Slide();
            Animator.SetBool("Falling", true);
        }
        else
            Animator.SetBool("Falling", false);
    }
    #region Input Functions
    private void OnJumpInput()
    {
        lastPressedJumpTime = Data.jumpInputBufferTime;
    }
    private void OnJumpUpInput()
    {
        if (CanJumpCut())
            _isJumpCut = true;
    }
    private void OnDashInput()
    {
        lastPressedDashTime = Data.dashInputBufferTime;
    }
    #endregion
    private void SetGravityScale(float scale)
    {
        _rb.gravityScale = scale;
    }
    private void Move()
    {
        Animator.SetFloat("Speed", Mathf.Abs(_moveInput.x));
        float targetSpeed = _moveInput.x * Data.runMaxSpeed;
        if (isSlowed)
            targetSpeed *= Data.runSlowMultiplier;
        float accelRate;
        if (lastOnGroundTime > 0)
            accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? Data.runAccelAmount : Data.runDeccelAmount;
        else
            accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? Data.runAccelAmount * Data.accelInAir : 
                Data.runDeccelAmount * Data.deccelInAir;

        if ((isJumping || _isJumpFalling) && Mathf.Abs(_rb.velocity.y) < Data.jumpHangTimeThreshold)
        {
            accelRate *= Data.jumpHangAccelerationMult;
            targetSpeed *= Data.jumpHangMaxSpeedMult;
        }
        if (Data.doConserveMomentum && Mathf.Abs(_rb.velocity.x) > Mathf.Abs(targetSpeed) && 
            Mathf.Sign(_rb.velocity.x) == Mathf.Sign(targetSpeed) && lastOnGroundTime < 0)
        {
            accelRate = 0;
        }
        float speedDif = targetSpeed - _rb.velocity.x;
        float movement = speedDif * accelRate;
        _rb.AddForce(movement * Vector2.right, ForceMode2D.Force);
    }
    private void Turn()
    {
        sprite.flipX = isFacingRight;
        isFacingRight = !isFacingRight;
        SendMessage("Turning");
    }
    private void Jump()
    {
        Animator.SetBool("Falling", false);
        Animator.SetTrigger("Jumping");
        lastPressedJumpTime = 0;
        lastOnGroundTime = 0;
        float force = Data.jumpForce;
        if (_rb.velocity.y < 0)
            force -= _rb.velocity.y;

        _rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
    }
    private void Dash()
    {
        Debug.Log("Dashed");
        Animator.SetBool("Rolling", true);
        lastPressedDashTime = 0;
        StartIFrames(Data.dashISeconds);
        float speedDif = Data.dashForce - Mathf.Abs(_rb.velocity.x);
        int direction;
        if (isFacingRight)
            direction = 1;
        else
            direction = -1;
        Debug.Log("diferencia de velocidad= " + speedDif + " y direccion= " + direction);
        _rb.AddForce(new Vector2(direction, 0.1f) * speedDif, ForceMode2D.Impulse);
    }
    public void EndDash()
    {
        Animator.SetBool("Rolling", false);
        isDashing = false;
    }
    private void Slide()
    {
        float speedDif = Data.slideSpeed - _rb.velocity.y;
        float movement = speedDif * Data.slideAccel;

        movement = Mathf.Clamp(movement, -Mathf.Abs(speedDif) * (1 / Time.fixedDeltaTime), 
            Mathf.Abs(speedDif) * (1 / Time.fixedDeltaTime));

        _rb.AddForce(movement * Vector2.up);
    }
    public void CheckDirectionToFace(bool isMovingRight)
    {
        if(isMovingRight != isFacingRight)
            Turn();
    }
    private bool CanJump()
    {
        return (lastOnGroundTime > 0 && !isJumping) || _jumpQuantity > 0;
    }
    private bool CanDash()
    {
        Debug.Log("Can dash?");
        return !isDashing;
    }
    private bool CanJumpCut()
    {
        return _rb.velocity.y > 0;
    }
    private bool CanSlide()
    {
        if (!isJumping && lastOnGroundTime <= 0)
            return true;
        else
            return false;
    }
    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_groundCheckPoint.position, _groundCheckSize);
    }
}
