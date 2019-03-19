using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public TotalEnemy totalEnemy;

    float time;

    void Start()
    {
        //StartCoroutine(SpawnEnemy());
    }

    private void Update()
    {
        if (totalEnemy.canSummon)
        {
            time += Time.deltaTime;
            if (time >= Random.Range(5f, 20f))
            {
                time = 0f;
                GameObject e = Instantiate(enemy, transform);
                e.GetComponent<BasicEnemy>().Damage *= totalEnemy.waves;
            }
        }

    }

    //IEnumerator SpawnEnemy()
    //{
    //    while(true)
    //    {
    //        if (totalEnemy.canSummon)
    //        {
    //            GameObject e = Instantiate(enemy, transform);
    //            e.GetComponent<BasicEnemy>().Damage *= totalEnemy.waves;
    //            yield return new WaitForSeconds(Random.Range(5f, 20f));
    //        }
    //    }
    //}
}
