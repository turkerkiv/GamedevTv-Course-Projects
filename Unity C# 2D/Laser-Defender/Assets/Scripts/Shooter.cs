using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject _projectilePrefab;
    [SerializeField] float _projectileSpeed = 10f;
    [SerializeField] float _projectileLifetime = 5f;
    [SerializeField] float _baseFiringRate = 1f;

    [Header("AI")]
    [SerializeField] bool usingAi;
    [SerializeField] float _firingRateVariance = 0f;
    [SerializeField] float _minimumFiringRate = 0.1f;

    public bool IsFiring { get; set; }

    Coroutine _firingCoroutine;
    AudioPlayer _audioPlayer;

    private void Awake()
    {
        _audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void Start()
    {
        if (usingAi)
        {
            IsFiring = true;
        }
    }

    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (IsFiring && _firingCoroutine == null)
        {
            _firingCoroutine = StartCoroutine(FireContinuously());
        }
        else if (!IsFiring && _firingCoroutine != null)
        {
            StopCoroutine(_firingCoroutine);
            _firingCoroutine = null;
        }
    }

    float GetFiringRate()
    {
        float firingRate = Random.Range(_baseFiringRate - _firingRateVariance, _baseFiringRate + _firingRateVariance);
        return Mathf.Clamp(firingRate, _minimumFiringRate, float.MaxValue);
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject cloneProjectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);

            Rigidbody2D cloneRb = cloneProjectile.GetComponent<Rigidbody2D>();
            if (cloneRb != null)
            {
                cloneRb.velocity = transform.up * _projectileSpeed;
            }

            Destroy(cloneProjectile, _projectileLifetime);

            _audioPlayer.PlayShootingClip();

            yield return new WaitForSeconds(GetFiringRate());
        }
    }
}
