using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private PlayerController playerController;
    public GameObject[] enemies;
    private GameObject enemy;
    private EnemyController enemyController;
    private bool noRepeat;

    private void Start()
    {
        int type = Random.Range(0, 2);
        enemy = (GameObject)Instantiate(enemies[type], transform);
        enemyController = enemy.GetComponent<EnemyController>();
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (!noRepeat && enemyController.enemyState == EnemyController.State.DYING && playerController.curHealth > 0)
            StartCoroutine(SpawnNewEnemy());
    }

    private IEnumerator SpawnNewEnemy()
    {
        noRepeat = true;
        yield return new WaitForSeconds(15.0f);
        int type = Random.Range(0, 2);
        enemy = (GameObject)Instantiate(enemies[type], transform);
        enemyController = enemy.GetComponent<EnemyController>();
        noRepeat = false;
    }
}
