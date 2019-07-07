﻿using UnityEngine;
using UnityEngine.AI;

public class EnemyControllerPlatformv2: MonoBehaviour
{
    public Transform[] patrolPoints;
    public int patrolPoint;

    public NavMeshAgent agent;
    public Animator animator;

    float bulletSpeed = 1100;
    public GameObject bullet;

    //AudioSource bulletAudio;

    public enum State
    {
        Idle,
        Patrol,
        Chase,
        Attack
    };
    public State state;

    public float waitAtPoint = 1f;
    public float chaseRange = 5f;
    public float attackRange = 1f;
    public float attackDelay = 2f;
    public float rotationSpeed = 3f;

    private float waitAtPointTimer;
    private float attackDelayTimer;

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(PlayerControllerPlatform.instance.transform.position, agent.transform.position);

        switch (state)
        {
            case State.Idle:
                Idle(distanceToPlayer);
                break;

            case State.Patrol:
                Patrol(distanceToPlayer);
                break;

            case State.Chase:
                Chase(distanceToPlayer);
                break;

            case State.Attack:
                Attack(distanceToPlayer);
                break;
        }
    }

    private void Idle(float distanceToPlayer)
    {
        if (distanceToPlayer <= chaseRange)
        {
            state = State.Chase;
        }
        else
        {
            animator.SetBool("IsMoving", false);

            if (waitAtPointTimer > 0)
            {
                waitAtPointTimer -= Time.deltaTime;
            }
            else
            {
                state = State.Patrol;
                agent.SetDestination(patrolPoints[patrolPoint].position);
            }
        }
    }

    private void Patrol(float distanceToPlayer)
    {
        if (distanceToPlayer <= chaseRange)
        {
            state = State.Chase;
        }
        else
        {


            bool isMoving = true;
            if (agent.remainingDistance <= 0.2f)
            {
                patrolPoint++;

                if (patrolPoint == patrolPoints.Length)
                    patrolPoint = 0;

                state = State.Idle;
                waitAtPointTimer += waitAtPoint;

                isMoving = false;
            }
            animator.SetBool("IsMoving", isMoving);
        }
    }

    private void Chase(float distanceToPlayer)
    {
        LookAtSlerp(PlayerControllerPlatform.instance.transform);

        agent.SetDestination(PlayerControllerPlatform.instance.transform.position);

        bool isMoving = true;
        if (distanceToPlayer <= attackRange)
        {
            state = State.Attack;

            isMoving = false;
            animator.SetTrigger("Attack");
            agent.velocity = Vector3.zero;
            agent.isStopped = true;

            attackDelayTimer = attackDelay;
        }
        else if (distanceToPlayer > chaseRange)
        {
            state = State.Patrol;
            waitAtPointTimer = waitAtPoint;
            agent.velocity = Vector3.zero;
            agent.SetDestination(agent.transform.position);
        }
        animator.SetBool("IsMoving", isMoving);
    }

    private void Attack(float distanceToPlayer)
    {
        LookAtSlerp(PlayerControllerPlatform.instance.transform);

        attackDelayTimer -= Time.deltaTime;

        if (attackDelayTimer <= 0)
        {
            if (distanceToPlayer <= attackRange)
            {
                animator.SetTrigger("Attack");
                attackDelayTimer = attackDelay;

                Fire();
    
    

    
}
            else
            {
                state = State.Idle;
                agent.isStopped = false;
            }
        }
    }

    void Fire()
    {
        //Shoot
        GameObject tempBullet = Instantiate(bullet, transform.position, transform.rotation) as GameObject;
        Rigidbody tempRigidBodyBullet = tempBullet.GetComponent<Rigidbody>();
        tempRigidBodyBullet.AddForce(tempRigidBodyBullet.transform.forward * bulletSpeed);
        Destroy(tempBullet, 0.5f);

        //Play Audio
        //bulletAudio.Play();

    }

    private void LookAtSlerp(Transform target)
    {
        agent.transform.rotation = Quaternion.Slerp(
            agent.transform.rotation, 
            Quaternion.LookRotation(target.transform.position - agent.transform.position), 
            Time.deltaTime * rotationSpeed
        );
        agent.transform.rotation = Quaternion.Euler(0f, agent.transform.rotation.eulerAngles.y, 0f);
    }
}
