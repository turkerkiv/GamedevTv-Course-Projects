using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] ParticleSystem _deathVFX;
    [SerializeField] int _score = 25;
    [SerializeField] int _hitPoints = 10;
    [SerializeField] AudioClip _expSFX;

    ScoreHandler _scoreHandler;
    Material _material;

    private void Awake()
    {
        _material = GetComponent<MeshRenderer>().material;
        _scoreHandler = FindObjectOfType<ScoreHandler>();

        gameObject.AddComponent<Rigidbody>().useGravity = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    private void ProcessHit()
    {
        _scoreHandler.IncreaseScore(_score);

        PlayHitVFX();
        PlayHitSFX();

        _hitPoints--;
        if (_hitPoints < 1)
        {
            KillEnemy();
        }
    }

    private void KillEnemy()
    {
        GameObject vfxInstance = Instantiate(_deathVFX.gameObject, transform.position, Quaternion.identity);
        Destroy(vfxInstance, _deathVFX.main.duration + 0.5f);
        Destroy(gameObject);
    }

    void PlayHitSFX()
    {
        AudioSource.PlayClipAtPoint(_expSFX, transform.position, 1f);
    }

    void PlayHitVFX()
    {
        _material.color = Color.red;
        Invoke(nameof(ChangeColorToDefault), 0.05f);
    }

    void ChangeColorToDefault()
    {
        _material.color = Color.white;
    }
}