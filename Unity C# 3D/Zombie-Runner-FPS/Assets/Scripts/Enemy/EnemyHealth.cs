using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float _hitPoints = 100f;

    CapsuleCollider _capsuleCollider;
    bool _isDead;
    public bool IsDead { get { return _isDead; } }

    public void TakeDamage(float amount)
    {
        if (_isDead) { return; }

        BroadcastMessage("OnDamageTaken");
        _hitPoints -= amount;

        if (_hitPoints <= Mathf.Epsilon)
        {
            Die();
        }
    }

    void Die()
    {
        _isDead = true;

        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("_die");

        _capsuleCollider = GetComponent<CapsuleCollider>();
        _capsuleCollider.center = new Vector3(0, 0.1f, 0);
        _capsuleCollider.direction = 2;
    }
}
