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
    private bool playerDetected;

    public float sinkSpeed;

    private Animator animator;
    private NavMeshAgent navMeshAgent;

    public enum State
    {
        ALIVE, DYING, SINKING
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        curHealth = maxHealth;
        navMeshAgent.isStopped = true;
    }

    void Update()
    {
        if (enemyState == State.ALIVE)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            if (distance <= detectRange)
            {
                navMeshAgent.SetDestination(player.transform.position);
                navMeshAgent.isStopped = false;
                playerDetected = true;
            }
            if (distance <= attackRange)
            {
                transform.LookAt(player.transform.position);
                navMeshAgent.isStopped = true;
                animator.SetBool("attack", true);
            }
            if (distance > attackRange && playerDetected)
            {
                navMeshAgent.SetDestination(player.transform.position);
                navMeshAgent.isStopped = false;
                animator.SetBool("attack", false);
            }
            if (navMeshAgent.isStopped)
                animator.SetBool("running", false);
            else
                animator.SetBool("running", true);
        }
        else if (enemyState == State.SINKING)
        {
            float sinkDistance = sinkSpeed * Time.deltaTime;
            transform.Translate(new Vector3(0, -sinkDistance, 0));
        }
    }

    public void Attacked()
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
        animator.SetTrigger("death");
    }

    private IEnumerator StartSinking()
    {
        yield return new WaitForSeconds(2.0f);
        navMeshAgent.enabled = false;
        enemyState = State.SINKING;
        Destroy(gameObject, 5);
    }
}
