using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed;
    private Vector3 initialPosition;
    private Sonic sonic;

    void Start()
    {
        speed = 2f;
        initialPosition = transform.position;
        sonic = GameObject.Find("Sonic").GetComponent<Sonic>();
    }

    void Update()
    {
        float precision = transform.position.y + 10;

        transform.Translate(Vector3.left * speed * Time.deltaTime);
        if (initialPosition.x - transform.position.x > 10)
        {
            Destroy(transform.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.name == "Sonic")
        {
            if (sonic.isInvincible == false && sonic.isHit == false)
                sonic.getHit();
        }
    }
}
