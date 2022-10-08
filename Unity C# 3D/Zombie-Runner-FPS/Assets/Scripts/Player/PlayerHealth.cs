using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float _health = 100f;

    DeathHandler _deathHandler;

    void Awake()
    {
        _deathHandler = GetComponent<DeathHandler>();
    }

    public void TakeDamage(float amount)
    {
        _health -= amount;
        _health = Mathf.Clamp(_health, 0, _health);

        Debug.Log("Current health is: " + _health);

        if (_health <= Mathf.Epsilon)
        {
            _deathHandler.HandleDeath();
        }
    }
}
