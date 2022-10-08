using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _runSpeed = 10f;
    [SerializeField] float _jumpSpeed = 5f;
    [SerializeField] float _climbSpeed = 5f;

    Vector2 _moveValue;
    Rigidbody2D _rigidbody;
    Animator _animator;
    CapsuleCollider2D _bodyCollider;
    BoxCollider2D _feetCollider;

    float _gravityScaleAtStart;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _bodyCollider = GetComponent<CapsuleCollider2D>();
        _feetCollider = GetComponent<BoxCollider2D>();
        
        _gravityScaleAtStart = _rigidbody.gravityScale;
    }

    void Update()
    {
        Run();
        FlipSprite();
        ClimbLadder();
    }

    void OnMove(InputValue value)
    {
        _moveValue = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (_feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            _rigidbody.velocity = new Vector2(0f, _jumpSpeed);
        }
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(_moveValue.x * _runSpeed, _rigidbody.velocity.y);
        _rigidbody.velocity = playerVelocity;

        if (Mathf.Abs(_moveValue.x) > Mathf.Epsilon)
        {
            _animator.SetBool("isRunning", true);
        }
        else
        {
            _animator.SetBool("isRunning", false);
        }
    }

    void FlipSprite()
    {
        if (Mathf.Abs(_moveValue.x) > Mathf.Epsilon)
        {
            transform.localScale = new Vector2(_moveValue.x, 1f);
        }
    }

    void ClimbLadder()
    {
        if (!_feetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            _rigidbody.gravityScale = _gravityScaleAtStart;
            _animator.SetBool("isClimbing", false);
            return;
        }

        Vector2 climbVelocity = new Vector2(_rigidbody.velocity.x, _moveValue.y * _climbSpeed);
        _rigidbody.velocity = climbVelocity;

        _rigidbody.gravityScale = 0f;

        bool playerHasVerticalSpeed = Mathf.Abs(_rigidbody.velocity.y) > Mathf.Epsilon;
        _animator.SetBool("isClimbing", playerHasVerticalSpeed);

    }
}
