using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LambentController : MonoBehaviour
{
    private int hp = 3;

    private NavMeshAgent agent;
    private Animator animator;
    private GameObject target;
    private bool targetFound;
    public bool isDead;
    EnemySpawner enemySpawner;
    PlayerController playerController;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        enemySpawner = transform.parent.gameObject.GetComponent<EnemySpawner>();
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    public void Attacked()
    {
        if (!isDead)
        {
            hp -= 1;
            if (hp <= 0)
                Killed();
        }
    }

    private void Update()
    {
        if (!isDead && targetFound)
        {
            agent.destination = target.transform.position;
            if (agent.remainingDistance <= 6.0f)
            {
                transform.LookAt(target.transform);
                agent.isStopped = true;
                animator.SetBool("running", false);
                animator.SetBool("attack", true);
            }
            else if (agent.remainingDistance > 6.0f)
            {
                agent.isStopped = false;
                animator.SetBool("attack", false);
                animator.SetBool("running", true);
            }
            if (hp <= 0)
                Killed();
        }

    }

    private void Killed()
    {
        agent.isStopped = true;
        isDead = true;
        animator.SetBool("death", true);
        enemySpawner.enemyCreated = false;
        playerController.enemyKilled = true;
        Destroy(gameObject, 2.0f);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!isDead && other.transform.tag == "Player")
        {
            target = other.gameObject;
            targetFound = true;
            agent.destination = target.transform.position;
            agent.isStopped = false;
            animator.SetBool("running", true);
        }
    }
}
