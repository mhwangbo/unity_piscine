using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public GameObject player;

    public float detectRange;
    public float attackRange;
    public State enemyState;
    public int maxHealth;
    private int curHealth;

    private Animator animator;
    private NavMeshAgent navMeshAgent;

    public enum State
    {
        ALIVE, DYING, SINKING
    }

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        curHealth = maxHealth;
    }

    void Update()
    {
        if (enemyState = State.ALIVE)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            if (distance <= detectRange)
                navMeshAgent.SetDestination(player.transform.position);
            if (distance <= attackRange)
            {
                animator.SetBool("Attack", true);
            }
        }

    }

    public void Attack()
    {
        if (enemyState == State.ALIVE)
        {
            curHealth -= 1;
            if (curHealth <= 0)
                Die();
        }
    }

    private void Die()
    {
        enemyState = State.DYING;
        navMeshAgent.isStopped = true;
        animator.SetTrigger("Dead");
    }
}
