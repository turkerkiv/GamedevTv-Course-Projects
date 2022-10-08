using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int _maxHitPoints = 5;
    
    [Tooltip("Adds amount to maxHitpoints when enemy dies")]
    [SerializeField] int _difficultyRamp = 1;

    Enemy _enemy;

    int _currentHitPoints;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    void OnEnable()
    {
        _currentHitPoints = _maxHitPoints;
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    void ProcessHit()
    {
        _currentHitPoints--;

        if (_currentHitPoints <= 0)
        {
            _enemy.RewardGold();
            _maxHitPoints += _difficultyRamp;
            gameObject.SetActive(false);
        }
    }
}
