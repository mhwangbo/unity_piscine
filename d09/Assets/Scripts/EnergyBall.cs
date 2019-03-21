using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBall : MonoBehaviour
{
    GameObject player;
    float time;

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        rb.AddForce((player.transform.position - transform.position) * 40.0f);
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time >= 5.0f)
            Destroy(this.gameObject);
    }
}
