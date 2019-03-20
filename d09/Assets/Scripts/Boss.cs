using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    BasicEnemy basicEnemy;

    void Start()
    {
        basicEnemy = GetComponent<BasicEnemy>();
    }

    void Update()
    {
        if(!basicEnemy.IsKilled)
        {

        }
    }
}
