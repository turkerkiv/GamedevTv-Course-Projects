using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightSystem : MonoBehaviour
{
    [SerializeField] float _lightDecay = .1f;
    [SerializeField] float _angleDecay = 1f;
    [SerializeField] float _minimumAngle = 40f;

    Light _myLight;

    private void Awake()
    {
        _myLight = GetComponent<Light>();
    }

    private void Update()
    {
        DecreaseLightAngle();
        DecreaseLightIntesity();
    }

    public void RestoreLightAngle(float restoreAngleAmount)
    {
        _myLight.spotAngle =  restoreAngleAmount;
    }

    public void RestoreLightIntensity(float restoreIntensityAmount)
    {
        _myLight.intensity = restoreIntensityAmount;
    }

    private void DecreaseLightIntesity()
    {
        _myLight.intensity -= _lightDecay * Time.deltaTime;
    }

    private void DecreaseLightAngle()
    {
        _myLight.spotAngle -= _angleDecay * Time.deltaTime;
        _myLight.spotAngle = Mathf.Clamp(_myLight.spotAngle, _minimumAngle, Mathf.Infinity);
    }


}
