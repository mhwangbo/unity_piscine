using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemies;
    private GameObject enemy;
    public bool enemyCreated;
    private LambentController enemyController;

    private void Start()
    {
        int type = Random.Range(0, 2);
        enemy = (GameObject)Instantiate(enemies[type], transform);
        enemyCreated = true;
    }

    private void Update()
    {
        if (!enemyCreated)
            StartCoroutine(SpawnNewEnemy());
    }

    private IEnumerator SpawnNewEnemy()
    {
        enemyCreated = true;
        yield return new WaitForSeconds(15.0f);
        int type = Random.Range(0, 2);
        enemy = (GameObject)Instantiate(enemies[type], transform);
    }
}
