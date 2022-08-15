using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : Health
{
    // Character movement values
    public MovementData Data;

    private Rigidbody2D _rb;
    private SpriteRenderer sprite;

    protected bool isFacingRight;
    protected bool isJumping;
    protected bool isSliding;
    protected float lastOnGroundTime;

    private bool _isJumpCut;
    private bool _isJumpFalling;

    private Vector2 _moveInput;
    private int _jumpQuantity;
    protected float lastPressedJumpTime;

    [SerializeField] private Transform _groundCheckPoint;
    [SerializeField] private Vector2 _groundCheckSize = new(0.49f, 0.03f);

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
    private void Update()
    {
        lastOnGroundTime -= Time.deltaTime;
        lastPressedJumpTime -= Time.deltaTime;

        _moveInput.x = Input.GetAxisRaw("Horizontal");
        _moveInput.y = Input.GetAxisRaw("Vertical");

        if (_moveInput.x != 0)
            CheckDirectionToFace(_moveInput.x > 0);
        if (Input.GetButtonDown("Jump"))
            OnJumpInput();
        if (Input.GetButtonUp("Jump"))
            OnJumpUpInput();

        if (!isJumping)
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

        if (CanJump() && lastPressedJumpTime > 0)
        {
            isJumping = true;
            _isJumpCut = false;
            _isJumpFalling = false;
            _jumpQuantity--;
            Jump();
        }

        if (CanSlide())
            isSliding = true;
        else
            isSliding = false;

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
    }
    private void FixedUpdate()
    {
        Move();
        if (isSliding)
            Slide();
    }
    private void OnJumpInput()
    {
        lastPressedJumpTime = Data.jumpInputBufferTime;
    }
    private void OnJumpUpInput()
    {
        if (CanJumpCut())
            _isJumpCut = true;
    }
    private void SetGravityScale(float scale)
    {
        _rb.gravityScale = scale;
    }
    private void Move()
    {
        float targetSpeed = _moveInput.x * Data.runMaxSpeed;

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
        if (Data.doConserveMomentum && Mathf.Abs(_rb.velocity.x) > 
            Mathf.Abs(targetSpeed) && Mathf.Sign(_rb.velocity.x) == Mathf.Sign(targetSpeed) && lastOnGroundTime < 0)
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
    }
    private void Jump()
    {
        lastPressedJumpTime = 0;
        lastOnGroundTime = 0;
        float force = Data.jumpForce;
        if (_rb.velocity.y < 0)
            force -= _rb.velocity.y;

        _rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
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
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_groundCheckPoint.position, _groundCheckSize);
    }
}
