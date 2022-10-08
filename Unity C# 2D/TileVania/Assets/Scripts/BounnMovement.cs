using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounnMovement : MonoBehaviour
{

    [SerializeField] float _moveSpeed = 1f;


    Rigidbody2D _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _rigidbody.velocity = new Vector2(_moveSpeed, 0f);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        _moveSpeed = -_moveSpeed;
        FlipEnemy();
    }

    void FlipEnemy()
    {
        transform.localScale = new Vector2(-transform.localScale.x, 1f);
    }
}
