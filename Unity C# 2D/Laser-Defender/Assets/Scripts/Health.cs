using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("General")]
    [SerializeField] bool _isPlayer;
    [SerializeField] int _health = 50;
    [SerializeField] int _deathScore = 50;

    [Header("Effects")]
    [SerializeField] ParticleSystem _hitEffect;
    [SerializeField] bool _willCameraShake;

    CameraShake _cameraShake;
    AudioPlayer _audioPlayer;
    ScoreKeeper _scoreKeeper;
    LevelManager _levelManager;

    void Awake()
    {
        _cameraShake = Camera.main.GetComponent<CameraShake>();
        _audioPlayer = FindObjectOfType<AudioPlayer>();
        _scoreKeeper = FindObjectOfType<ScoreKeeper>();
        _levelManager = FindObjectOfType<LevelManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //health mermide olmadığı için ve burada other yani healthın karşısındakini aldığımız için damagedealer olsa da enemye bir şey olmuyor çarpmada.
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        if (damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
            PlayHitEffect();
            ShakeCamera();
            _audioPlayer.PlayExplosionClip();
            damageDealer.Hit();
        }
    }

    public int GetHealth()
    {
        return _health;
    }

    void ShakeCamera()
    {
        if (_cameraShake != null && _willCameraShake)
        {
            _cameraShake.PlayShaking();
        }
    }

    void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (!_isPlayer)
        {
            _scoreKeeper.ModifyScore(_deathScore);
        }
        else
        {
            _levelManager.LoadGameOverMenu();
        }

        Destroy(gameObject);
    }

    void PlayHitEffect()
    {
        if (_hitEffect != null)
        {
            ParticleSystem instanceHE = Instantiate(_hitEffect, transform.position, Quaternion.identity);
            Destroy(instanceHE.gameObject, instanceHE.main.duration + instanceHE.main.startLifetime.constantMax);
        }
    }
}
