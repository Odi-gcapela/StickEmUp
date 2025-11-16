using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : MonoBehaviour
{
    public enum EnemyState {
    Idle,
    Chase,
    Attack
    }

    public EnemyState currentState = EnemyState.Idle;

    private Rigidbody EnemyRigidbody;
    private Transform playerTarget;
    private float attackDelay = 0f;

    [SerializeField]
    public float moveSpeed = 8f;
    public float attackRange = 2f;
    public float attackRate = 1.2f;

    // Trigger functions for detection
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("COLLISION DETECTED! Switching to Chase state.", this);

            currentState = EnemyState.Chase;
            playerTarget = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("COLLISION DETECTED! Switching to Idle state.", this);
            currentState = EnemyState.Idle;
            playerTarget = null;
        }
    }

    void Start()
    {
        EnemyRigidbody = GetComponent<Rigidbody>();
        WhereIsPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case EnemyState.Idle:
                break;
            case EnemyState.Chase:
                transform.LookAt(playerTarget.position);
                ChasePlayer();
                break;
            case EnemyState.Attack:
                AttackPlayer();
                break;
        }
    }

    void WhereIsPlayer()
    {
        SphereCollider detectionSphere = GetComponent<SphereCollider>();

        if (detectionSphere == null)
        {
            Debug.LogError("Goblin doesn't have proper sphere collider.");
            return;
        }

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionSphere.radius);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                currentState = EnemyState.Chase;
                playerTarget = hitCollider.transform;
                return;
            }
        }
    }

    void ChasePlayer()
    {
        if (playerTarget == null) return;

        float distance = Vector3.Distance(transform.position, playerTarget.position);

        if (distance <= attackRange)
        {
            EnemyRigidbody.velocity = Vector3.zero;
            currentState = EnemyState.Attack;
        }
        else
        {
            Vector3 direction = (playerTarget.position - transform.position).normalized;
            direction.y = 0;

            EnemyRigidbody.velocity = direction * moveSpeed;
            
            Quaternion lookingRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookingRotation, Time.deltaTime * 10f);
        }
    }

    void AttackPlayer()
    {
        if (playerTarget == null) return;

        float distance = Vector3.Distance(transform.position, playerTarget.position);

        if (distance > attackRange + 0.2f)
        {
            currentState = EnemyState.Chase;
            return;
        }

        if (Time.time >= attackDelay)
        {
            Attack();
            attackDelay = Time.time + 2f / attackRate;
        }
    }
    
    void Attack()
    {
        Debug.Log("Enemy is attacking!!");
    }
}
