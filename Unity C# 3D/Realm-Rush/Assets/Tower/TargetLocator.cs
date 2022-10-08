using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform _weapon;
    [SerializeField] float _shootRange = 15f;
    [SerializeField] ParticleSystem _shootingParticle;

    Transform _target;


    private void Awake()
    {
    }

    private void Update()
    {
        FindClosestTarget();
        AimWeapon();
    }

    void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        float maxDistance = Mathf.Infinity;
        Transform closestTarget = null;

        foreach (Enemy enemy in enemies)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);

            if (targetDistance < maxDistance)
            {
                closestTarget = enemy.transform;
                maxDistance = targetDistance;
            }
        }

        _target = closestTarget;
    }

    void AimWeapon()
    {
        if (_target == null)
        {
            Shoot(false);
            return;
        }

        _weapon.LookAt(_target);

        float targetDistance = Vector3.Distance(transform.position, _target.position);
        if (targetDistance < _shootRange)
        {
            Shoot(true);
        }
        else
        {
            Shoot(false);
        }
    }

    private void Shoot(bool isActive)
    {
        var shootEmission = _shootingParticle.emission;
        shootEmission.enabled = isActive;
    }
}
