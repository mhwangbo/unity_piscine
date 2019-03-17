using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float range;

    private Vector3 startPosition;
    private Rigidbody2D rb2d;

    private void Start()
    {
        startPosition = transform.position;
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        if (Vector3.Distance(startPosition, transform.position) > range)
            Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);     
    }
}
