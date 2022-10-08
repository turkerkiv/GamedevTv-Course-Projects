using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPickup : MonoBehaviour
{
    [SerializeField] float _restoreAngleAmount = 70f;
    [SerializeField] float _restoreIntensityAmount = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FlashlightSystem flSystem = other.GetComponentInChildren<FlashlightSystem>();
            flSystem.RestoreLightAngle(_restoreAngleAmount);
            flSystem.RestoreLightIntensity(_restoreIntensityAmount);

            Destroy(gameObject);
        }
    }
}
