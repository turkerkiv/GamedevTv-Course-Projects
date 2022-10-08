using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 5f;

    [SerializeField] Vector3 _paddingBottomLeft;
    [SerializeField] Vector3 _paddingTopRight;

    Vector3 _rawInput;

    Vector2 _minBounds;
    Vector2 _maxBounds;

    Shooter _shooter;

    void Awake()
    {
        _shooter = GetComponent<Shooter>();
    }

    void Start()
    {
        InitBounds();
    }

    void Update()
    {
        Move();
    }

    void InitBounds()
    {
        Camera mainCamera = Camera.main;
        _minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0)) + _paddingBottomLeft;
        _maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1)) + _paddingTopRight;
    }

    void Move()
    {
        Vector2 delta = _rawInput * _moveSpeed * Time.deltaTime;
        Vector2 newPos = new Vector2(0, 0);
        newPos.x = Mathf.Clamp(transform.position.x + delta.x, _minBounds.x, _maxBounds.x);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, _minBounds.y, _maxBounds.y);
        transform.position = newPos;
    }

    void OnMove(InputValue value)
    {
        _rawInput = value.Get<Vector2>();
    }

    void OnFire(InputValue value)
    {
        if(_shooter != null)
        {
            _shooter.IsFiring = value.isPressed;
        }
    }
}
