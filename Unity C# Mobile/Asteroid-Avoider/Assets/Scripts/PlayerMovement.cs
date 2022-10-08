using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _forceMagnitude;
    [SerializeField] float _maxVelocity = 6f;
    [SerializeField] float _rotationSpeed = 5f;

    Camera _mainCamera;
    Rigidbody _rb;

    Vector3 _movementDirection;

    void Start()
    {
        _mainCamera = Camera.main;
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        GetMovementDirection();

        KeepPlayerOnScreen();

        RotateToFaceVelocity();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void GetMovementDirection()
    {
        if (!Touchscreen.current.primaryTouch.press.isPressed)
        {
            _movementDirection = Vector3.zero;
            return;
        }

        Vector2 touchPosInPixel = Touchscreen.current.primaryTouch.position.ReadValue();
        Vector3 touchPosInWorld = _mainCamera.ScreenToWorldPoint(touchPosInPixel);

        _movementDirection = transform.position - touchPosInWorld;
        _movementDirection.z = 0f;
        _movementDirection.Normalize();
    }

    void MovePlayer()
    {
        if (_movementDirection == Vector3.zero) { return; }

        _rb.AddForce(_movementDirection * _forceMagnitude, ForceMode.Force);
        _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, _maxVelocity);
    }

    void KeepPlayerOnScreen()
    {
        Vector3 newPosition = transform.position;

        Vector3 playerPosInViewport = _mainCamera.WorldToViewportPoint(transform.position);
        if (playerPosInViewport.x > 1)
        {
            newPosition.x = -newPosition.x + 0.1f;
        }
        else if (playerPosInViewport.x < 0)
        {
            newPosition.x = -newPosition.x - 0.1f;
        }
        if (playerPosInViewport.y > 1)
        {
            newPosition.y = -newPosition.y + 0.1f;
        }
        else if (playerPosInViewport.y < 0)
        {
            newPosition.y = -newPosition.y - 0.1f;
        }

        transform.position = newPosition;
    }

    void RotateToFaceVelocity()
    {
        if (_rb.velocity == Vector3.zero) { return; }

        Quaternion targetRotation = Quaternion.LookRotation(_rb.velocity, Vector3.back);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
    }
}
