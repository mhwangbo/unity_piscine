using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public GameObject player;
    private PlayerController playerController;

    public float detectRange;
    public float attackRange;
    public State enemyState;
    [HideInInspector] public float curHealth;
    private bool playerDetected;

    public float sinkSpeed;

    private Animator animator;
    private NavMeshAgent navMeshAgent;

    // enemy stat
    public CharacterStat stat;

    public GameObject potion;

    public enum State
    {
        ALIVE, DYING, SINKING
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        navMeshAgent.isStopped = true;
        stat = new CharacterStat(Random.Range(5, 8), Random.Range(5, 8), Random.Range(5, 8));
        int playerLevel = playerController.stat.Level;
        for (int i = 1; i < playerLevel; i++)
            stat.EnemyLevelUp();
        curHealth = stat.HP;
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

    public void Attacked(float damage)
    {
        if (enemyState == State.ALIVE)
        {
            curHealth -= damage;
            if (curHealth <= 0)
                Die();
        }
    }

    private void Die()
    {
        playerController.stat.EXP = stat.HP / 5;
        enemyState = State.DYING;
        navMeshAgent.isStopped = true;
        animator.SetTrigger("death");
    }

    private IEnumerator StartSinking()
    {
        yield return new WaitForSeconds(2.0f);
        navMeshAgent.enabled = false;
        enemyState = State.SINKING;
        yield return new WaitForSeconds(3.0f);
        GameObject potionPre = null;
        if (Random.value <= 0.5f)
        {
            potionPre = Instantiate(potion);
            potionPre.transform.position = new Vector3(transform.position.x, transform.position.y + 7.0f, transform.position.z);
        }
        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
    }

    public void Attack()
    {
        float hitChance = stat.HitChance(playerController.stat.Agility);
        float random = Random.value;
        if (random <= hitChance / 100)
            playerController.Attacked(stat.FinalDamage(0.0f));
        if (playerController.curHealth <= 0)
            StartCoroutine(StartSinking());
    }
}
