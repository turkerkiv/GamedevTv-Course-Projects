using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarMovement : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 5f;
    [SerializeField] float _speedGainEverySecond = 0.3f;
    [SerializeField] float _steerGainEverySecond = 0.6f;
    [SerializeField] float _turnSpeed = 10f;

    int _steerValue;

    void Start()
    {
        if (PlayerPrefs.GetInt(Store.NewCarUnlockedKey, 0) == 1)
        {
            GetComponentInChildren<Renderer>().material.color = Color.blue;
        }
    }

    void Update()
    {
        _moveSpeed += _speedGainEverySecond * Time.deltaTime;
        _turnSpeed += _steerGainEverySecond * Time.deltaTime;

        transform.Rotate(Vector3.up * _steerValue * Time.deltaTime * _turnSpeed);
        transform.Translate(Vector3.forward * Time.deltaTime * _moveSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void Steer(int value)
    {
        _steerValue = value;
    }
}
