using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turret : MonoBehaviour
{

    public GameObject bullet;
    private float timer = 0.0f;
    private float waitTime = 0.3f;


    void Update()
    {
        timer += Time.deltaTime;
        if (timer > waitTime)
        {
            timer -= waitTime;
            GameObject.Instantiate(bullet);
        }
    }
}
