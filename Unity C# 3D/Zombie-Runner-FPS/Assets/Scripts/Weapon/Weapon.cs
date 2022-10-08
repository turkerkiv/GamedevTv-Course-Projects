using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera _fPCamera;
    [SerializeField] float _range = 100f;
    [SerializeField] float _damage = 30f;
    [SerializeField] ParticleSystem _muzzleVFX;
    [SerializeField] ParticleSystem _hitVFX;
    [SerializeField] Ammo _ammo;
    [SerializeField] AmmoType _ammoType;

    [SerializeField] float _shootDelay = 0f;

    [SerializeField] TextMeshProUGUI _ammoText;

    bool _canShoot = true;

    private void OnEnable()
    {
        _canShoot = true;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && _canShoot)
        {
            StartCoroutine(Shoot());
        }

        DisplayAmmo();
    }

    void DisplayAmmo()
    {
        int currentAmmo = _ammo.GetCurrentAmmo(_ammoType);

        _ammoText.text = currentAmmo.ToString();
    }

    IEnumerator Shoot()
    {
        _canShoot = false;
        if (_ammo.GetCurrentAmmo(_ammoType) > 0)
        {
            _ammo.DecreaseCurrentAmmo(_ammoType);
            PlayMuzzleVFX();
            ProcessRaycast();
        }

        yield return new WaitForSeconds(_shootDelay);
        _canShoot = true;
    }

    private void PlayMuzzleVFX()
    {
        _muzzleVFX.Play();
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(_fPCamera.transform.position, _fPCamera.transform.forward, out hit, _range))
        {
            PlayHitVFX(hit);

            EnemyHealth enemy = hit.transform.GetComponent<EnemyHealth>();
            if (enemy == null) { return; }
            enemy.TakeDamage(_damage);
        }
        else
        {
            return;
        }
    }

    private void PlayHitVFX(RaycastHit hit)
    {
        ParticleSystem instancePS = Instantiate(_hitVFX, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(instancePS.gameObject, _hitVFX.main.startLifetime.constant);
    }
}
