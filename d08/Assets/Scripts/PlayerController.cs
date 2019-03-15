using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    private bool targetEnemy;
    private GameObject enemy;
    private LambentController enemyController;
    private Coroutine coroutine;
    private bool attackStart;
    private bool firstAttack;
    private LambentController tmpEnemyController;
    public bool enemyKilled;
    private bool attackTiming;
    private bool animationEnd;
    private Coroutine oneAttack;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                agent.isStopped = false;
                agent.destination = hit.point;
                animator.SetBool("running", true);
                if (hit.transform.tag == "Enemy")
                {
                    targetEnemy = true;
                    firstAttack = true;
                    enemy = hit.transform.gameObject;
                    enemyController = enemy.GetComponent<LambentController>();
                    enemyKilled = false;
                }
                else
                {
                    targetEnemy = false;
                    enemy = null;
                    enemyController = null;
                    ResetAttack();
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
            ResetAttack();
        if (agent.remainingDistance <= 1.0f)
        {
            agent.isStopped = true;
            animator.SetBool("running", false);
            if (targetEnemy && !attackStart)
                coroutine = StartCoroutine(StartAttack());
            if (firstAttack && !targetEnemy && !animator.GetBool("attack"))
                oneAttack = StartCoroutine(AttackOnce());
        }
    }

    public void AttackTiming(int s)
    {
        if (s == 1)
            attackTiming = true;
        if (s == 2)
            animationEnd = true;
    }

    private IEnumerator AttackOnce()
    {
        animator.SetBool("attack", true);
        yield return new WaitUntil(() => attackTiming);
        tmpEnemyController.Attacked();
        attackTiming = false;
        yield return new WaitUntil(() => animationEnd);
        animator.SetBool("attack", false);
        animationEnd = false;
        firstAttack = false;
        ResetAttack();
    }

    private IEnumerator StartAttack()
    {
        attackStart = true;
        animator.SetBool("attack", true);
        while (true)
        {
            if (!firstAttack)
                firstAttack = false;
            yield return new WaitUntil(() => attackTiming);
            enemyController.Attacked();
            attackTiming = false;
            yield return new WaitUntil(() => animationEnd);
            animationEnd = false;
            if (!enemy)
                ResetAttack();
        }
    }

    private void ResetAttack()
    {
        tmpEnemyController = enemyController;
        targetEnemy = false;
        enemy = null;
        animator.SetBool("attack", false);
        if (attackStart)
        {
            StopCoroutine(coroutine);
            attackStart = false;
        }
        if (firstAttack && animator.GetBool("attack"))
        {
            StopCoroutine(oneAttack);
            firstAttack = false;
        }
    }
}