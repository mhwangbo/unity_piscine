using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Animator animator;

    // Target enemy management
    private GameObject enemy;
    private EnemyController enemyController;
    private bool enemySet;
    private bool mouseReleased;

    public float attackRange;

    // Player Stat
    private CharacterStat stat;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        stat = new CharacterStat(Random.Range(10, 20), Random.Range(10, 20), Random.Range(10, 20));
        print(stat.HP);
    }

    private void Update()
    {
        float distance = 100.0f;

        if (enemySet)
            distance = Vector3.Distance(enemy.transform.position, transform.position);
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                navMeshAgent.SetDestination(hit.point);
                navMeshAgent.isStopped = false;
                if (hit.transform.tag == "Enemy")
                {
                    enemySet = true;
                    mouseReleased = false;
                    enemy = hit.transform.gameObject;
                    enemyController = enemy.GetComponent<EnemyController>();
                }
                else
                    enemySet = false;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            mouseReleased = true;
        }
        if (enemySet && distance <= attackRange)
        {
            Vector3 enemyPosition = new Vector3(enemy.transform.position.x, transform.position.y, enemy.transform.position.z);
            transform.LookAt(enemyPosition);
            navMeshAgent.isStopped = true;
            if (enemyController.enemyState == EnemyController.State.ALIVE)
            {
                animator.SetTrigger("attack");
                if (mouseReleased == true)
                    enemySet = false;
            }
        }
        if (!enemySet && navMeshAgent.remainingDistance <= 1.0f)
            navMeshAgent.isStopped = true;

        if (navMeshAgent.isStopped)
            animator.SetBool("running", false);
        else
            animator.SetBool("running", true);
    }

    public void Attack()
    {
        enemyController.Attacked();
    }
}