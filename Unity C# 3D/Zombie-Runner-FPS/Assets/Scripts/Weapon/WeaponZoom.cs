using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] Camera _playerCam;
    [SerializeField] RigidbodyFirstPersonController _fpsController;

    [SerializeField] float _zoomedOutFOV = 60f;
    [SerializeField] float _zoomedInFOV = 30f;

    [SerializeField] float _zoomedOutSensitivity = 2f;
    [SerializeField] float _zoomedInSensitivity = 1f;


    bool _isZoomedIn;

    private void Start()
    {
        _zoomedOutSensitivity = _fpsController.mouseLook.XSensitivity;
    }

    void Update()
    {
        Zoom();
    }

    private void OnDisable()
    {
        ZoomOut();
    }

    public void Zoom()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (!_isZoomedIn)
            {
                ZoomIn();
            }
            else
            {
                ZoomOut();
            }
        }
    }

    private void ZoomOut()
    {
        _playerCam.fieldOfView = _zoomedOutFOV;
        _isZoomedIn = false;

        _fpsController.mouseLook.XSensitivity = _zoomedOutSensitivity;
        _fpsController.mouseLook.YSensitivity = _zoomedOutSensitivity;
    }

    private void ZoomIn()
    {
        _playerCam.fieldOfView = _zoomedInFOV;
        _isZoomedIn = true;

        _fpsController.mouseLook.XSensitivity = _zoomedInSensitivity;
        _fpsController.mouseLook.YSensitivity = _zoomedInSensitivity;
    }
}
