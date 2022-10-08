using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _boostSpeed = 32f;
    [SerializeField] float _normalSpeed = 16f;
    [SerializeField] int _boostAmount = 100;
    [SerializeField] float _torqueAmount = 1f;
    private Rigidbody2D _rigidbody2D;
    private SurfaceEffector2D _surfaceEffector2D;
    private bool _canMove = true;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _surfaceEffector2D = FindObjectOfType<SurfaceEffector2D>();
    }

    void Update()
    {
        if (_canMove)
        {
            RotatePlayer();
            RespondToBoost();
        }
    }

    public void DisableControls()
    {
        _canMove = false;
    }
    private void RespondToBoost()
    {
        if (Input.GetKey(KeyCode.UpArrow) && _boostAmount > 0)
        {
            _surfaceEffector2D.speed = _boostSpeed;
            _boostAmount--;
            Debug.Log(_boostAmount);
        }
        else
        {
            _surfaceEffector2D.speed = _normalSpeed;
        }
    }

    private void RotatePlayer()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _rigidbody2D.AddTorque(_torqueAmount);
        }

        else if (Input.GetKey(KeyCode.RightArrow))
        {
            _rigidbody2D.AddTorque(-_torqueAmount);
        }
    }
}
