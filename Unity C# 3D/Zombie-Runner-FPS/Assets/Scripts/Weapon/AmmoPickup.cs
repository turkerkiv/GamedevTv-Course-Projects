using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] int _ammoAmount = 5;
    [SerializeField] AmmoType _ammoType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Ammo ammo = other.GetComponent<Ammo>();
            ammo.IncreaseCurrentAmmo(_ammoType, _ammoAmount);

            Destroy(gameObject);
        }
    }
}
