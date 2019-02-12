using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    private float speed;
    private Vector3 initialPosition;

    void Start()
    {
        speed = 2f;
        initialPosition = transform.position;
    }

    void Update()
    {
        float precision = transform.position.y + 4;

        transform.Translate(Vector3.left * speed * Time.deltaTime);
        if (initialPosition.x - transform.position.x > 4)
        {
            Destroy(transform.gameObject);
        }
    }
}
