using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] float _damage = 40f;

    PlayerHealth _target;
    EnemyHealth _enemyHealth;

    void Awake()
    {
        _target = FindObjectOfType<PlayerHealth>();
        _enemyHealth = GetComponent<EnemyHealth>();
    }

    void AttackHitEvent()
    {
        if (_enemyHealth.IsDead) { return;}

        if (_target == null) { return; }

        _target.TakeDamage(_damage);
        _target.GetComponent<DisplayDamage>().ShowDamageImpact();
    }
}
