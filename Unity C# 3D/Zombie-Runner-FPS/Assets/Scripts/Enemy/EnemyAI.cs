using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] float _chaseRange = 5f;
    [SerializeField] float _turnSpeed = 5f;
    
    Transform _target;
    Animator _animator;
    NavMeshAgent _navMeshAgent;
    EnemyHealth _enemyHealth;


    float _distanceToTarget = Mathf.Infinity;
    bool _isProvoked;

    void Awake()
    {
        _target = FindObjectOfType<PlayerHealth>().transform;
        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _enemyHealth = GetComponent<EnemyHealth>();
    }

    void Update()
    {
        if (_enemyHealth.IsDead)
        {
            enabled = false;
            _navMeshAgent.enabled = false;
            return;
        }

        _distanceToTarget = Vector3.Distance(_target.position, transform.position);

        if (_isProvoked)
        {
            EngageTarget();
        }
        else if (_distanceToTarget <= _chaseRange)
        {
            _isProvoked = true;
        }
        else
        {
            _animator.SetTrigger("_idle");
        }
    }

    public void OnDamageTaken()
    {
        _isProvoked = true;
    }

    void EngageTarget()
    {
        FaceTarget();
        ChaseTarget();

        if (_distanceToTarget <= _navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
    }

    void ChaseTarget()
    {
        _animator.SetBool("_attack", false);
        _animator.SetTrigger("_move");

        _navMeshAgent.SetDestination(_target.position);
    }

    void AttackTarget()
    {
        _animator.SetBool("_attack", true);
    }

    void FaceTarget()
    {
        Vector3 direction = (_target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * _turnSpeed);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _chaseRange);
    }
}
