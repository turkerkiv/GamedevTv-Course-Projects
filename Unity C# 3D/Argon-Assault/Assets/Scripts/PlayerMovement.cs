using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Controls")]
    [SerializeField] InputAction _movement;
    [SerializeField] InputAction _firing;

    [Header("Laser gun Array")]
    [SerializeField] ParticleSystem[] _lasers;

    [Header("General Setup Settings")]
    [Tooltip("How fast ship moves up and down")][SerializeField] float _controlSpeed = 2f;
    [SerializeField] Vector2 _boundary;

    [Header("Screen position based tuning")]
    [SerializeField] float _positionPitchFactor = -2f;
    [SerializeField] float _positionYawFactor = 2f;

    [Header("Player input based tuning")]
    [SerializeField] float _controlPitchFactor = -10f;
    [SerializeField] float _controlRollFactor = -10f;

    [SerializeField] float _rotationFactor = 1f;

    float _xThrow, _yThrow;

    void Start()
    {

    }

    private void OnEnable()
    {
        _movement.Enable();
        _firing.Enable();
    }

    private void OnDisable()
    {
        _movement.Disable();
        _firing.Disable();
    }

    void Update()
    {
        MoveShip();
        RotateShip();
        ProcessFiring();
    }

    private void RotateShip()
    {
        float pitchDueToPosition = transform.localPosition.y * _positionPitchFactor;
        float pitchDueToControl = _yThrow * _controlPitchFactor;

        float pitch = pitchDueToPosition + pitchDueToControl;
        float yaw = transform.localPosition.x * _positionYawFactor;
        float roll = _xThrow * _controlRollFactor;

        //eski input sisteminde mode analog olduğu için smooth dönüyor ama yenisinde 0 1 olunca ani oluyor onun için bu kodlar.
        Quaternion targetRotation = Quaternion.Euler(pitch, yaw, roll);
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, _rotationFactor);
    }

    void MoveShip()
    {
        _xThrow = _movement.ReadValue<Vector2>().x;
        _yThrow = _movement.ReadValue<Vector2>().y;

        float newXPos = transform.localPosition.x + (_xThrow * _controlSpeed * Time.deltaTime);
        float newYPos = transform.localPosition.y + (_yThrow * _controlSpeed * Time.deltaTime);

        float clampedXPos = Mathf.Clamp(newXPos, -_boundary.x, _boundary.x);
        float clampedYPos = Mathf.Clamp(newYPos, -_boundary.y, _boundary.y);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, 0);
    }

    void ProcessFiring()
    {
        if (_firing.IsPressed())
        {
            SetLaserActive(true);
        }
        else
        {
            SetLaserActive(false);
        }
    }

    void SetLaserActive(bool isActive)
    {
        foreach (ParticleSystem laser in _lasers)
        {
            var emissionModule = laser.emission;
            emissionModule.enabled = isActive;
        }
    }
}
