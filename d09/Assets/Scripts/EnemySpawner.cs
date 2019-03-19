using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public TotalEnemy totalEnemy;


    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while(true)
        {
            if (totalEnemy.canSummon)
            {
                GameObject e = Instantiate(enemy, transform);
                e.GetComponent<BasicEnemy>().Damage *= totalEnemy.waves;
                yield return new WaitForSeconds(Random.Range(5f, 20f));
            }
        }
    }
}
