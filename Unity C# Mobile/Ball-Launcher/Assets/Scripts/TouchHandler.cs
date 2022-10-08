using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class TouchHandler : MonoBehaviour
{
    [SerializeField] GameObject _ballPrefab;
    [SerializeField] Rigidbody2D _pivotRb;
    [SerializeField] float _respawnDelay = 2f;

    Rigidbody2D _currentBallRb;
    SpringJoint2D _currentBallSpringJoint;
    Camera _mainCamera;
    Vector2 _ballPosition;
    bool _isDragging;
    GameObject _ballInstance;

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    void Start()
    {
        _mainCamera = Camera.main;

        InstansiateNewBall();
    }

    void Update()
    {
        if (_currentBallRb == null)
            return;

        if (Touch.activeTouches.Count == 0)
        {
            if (_isDragging)
            {
                LaunchBall();
                _isDragging = false;
            }
            return;
        }

        _isDragging = true;
        _currentBallRb.isKinematic = true;

        Vector2 touchPositionAtScreen = new Vector2();
        foreach (Touch touch in Touch.activeTouches)
        {
            touchPositionAtScreen += touch.screenPosition;
        }
        touchPositionAtScreen /= Touch.activeTouches.Count;
        
        Vector2 touchPositionAtWorld = _mainCamera.ScreenToWorldPoint(touchPositionAtScreen);
        _currentBallRb.position = touchPositionAtWorld;
    }

    void LaunchBall()
    {
        _currentBallRb.isKinematic = false;
        _currentBallRb = null;

        Invoke(nameof(DetachBall), 0.1f);
    }

    void DetachBall()
    {
        _currentBallSpringJoint.enabled = false;
        _currentBallSpringJoint = null;

        Invoke(nameof(InstansiateNewBall), _respawnDelay);
        Destroy(_ballInstance, 5f);
    }

    void InstansiateNewBall()
    {
        _ballInstance = Instantiate(_ballPrefab, _pivotRb.position, Quaternion.identity, _pivotRb.transform.parent);

        _currentBallRb = _ballInstance.GetComponent<Rigidbody2D>();
        _currentBallSpringJoint = _ballInstance.GetComponent<SpringJoint2D>();

        _currentBallSpringJoint.connectedBody = _pivotRb;
    }
}
