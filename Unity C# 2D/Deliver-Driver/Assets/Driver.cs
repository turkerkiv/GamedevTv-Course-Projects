using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{
    [SerializeField] float _steerSpeed = 130f;
    [SerializeField] float _moveSpeed = 20f;
    [SerializeField] float _slowSpeed = 15f;
    [SerializeField] float _boostSpeed = 30f;

    // Update is called once per frame
    void Update()
    {
        float steerAmount= Input.GetAxis("Horizontal") * _steerSpeed * Time.deltaTime;
        float moveAmount = Input.GetAxis("Vertical") * _moveSpeed*Time.deltaTime;

        transform.Rotate(0,0,-steerAmount);
        transform.Translate(0,moveAmount,0);
    }
    void OnCollisionEnter2D(Collision2D other) 
    {
        Debug.Log("You crashed");
        _moveSpeed = _slowSpeed;
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Boost")
        {
            Debug.Log("You took some boost!");
            _moveSpeed = _boostSpeed;
        }   
    }
}
