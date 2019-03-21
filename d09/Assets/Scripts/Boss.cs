using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    BasicEnemy basicEnemy;
    float time;
    public GameObject energyBall;
    GameObject energyHolder;

    GameObject eb;
    Vector3 dir;

    void Start()
    {
        basicEnemy = GetComponent<BasicEnemy>();
        energyHolder = transform.Find("GameObject").gameObject;
    }

    void Update()
    {
        time += Time.deltaTime;
        float distance = Vector3.Distance(basicEnemy.player.transform.position, transform.position);
        if(distance >= 4.0f && !basicEnemy.IsKilled && time >= Random.Range(3.0f, 5.0f))
        {
            time = 0f;
            ThrowEnergyBall();
        }
    }

    void ThrowEnergyBall()
    {
        eb = Instantiate(energyBall, energyHolder.transform);
        eb.transform.localPosition = Vector3.zero;
        eb.name = "EnergyBall";
    }
}
